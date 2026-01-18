using WebAPI_NOVOAssignment.DTOs;
using WebAPI_NOVOAssignment.Models;
using WebAPI_NOVOAssignment.Repositories.Interfaces;
using WebAPI_NOVOAssignment.Services.Interfaces;
using WebAPI_NOVOAssignment.Utilities;

namespace WebAPI_NOVOAssignment.Services;

/// <summary>
/// Service implementation for authentication operations
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtTokenGenerator _tokenGenerator;

    /// <summary>
    /// Initializes a new instance of the AuthService
    /// </summary>
    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        JwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenGenerator = tokenGenerator;
    }
   
    public async Task<AuthResultDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            return null;

        var accessToken = _tokenGenerator.GenerateAccessToken(user);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        var expiresAt = _tokenGenerator.GetAccessTokenExpiration();

        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            ExpiryDate = _tokenGenerator.GetRefreshTokenExpiration(),
            IsRevoked = false,
            CreatedDate = DateTime.UtcNow
        };

        await _refreshTokenRepository.AddAsync(refreshTokenEntity);

        return new AuthResultDto
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
            Roles = user.Roles.Select(r => r.Name).ToList()
        };
    }

   
    public async Task<AuthResultDto?> RefreshTokenAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        
        if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
            return null;

        var user = await _userRepository.GetByIdAsync(token.UserId);
        if (user == null)
            return null;

        var newAccessToken = _tokenGenerator.GenerateAccessToken(user);
        var newRefreshToken = _tokenGenerator.GenerateRefreshToken();
        var expiresAt = _tokenGenerator.GetAccessTokenExpiration();

        // Revoke old refresh token
        token.IsRevoked = true;
        await _refreshTokenRepository.UpdateAsync(token);

        // Create new refresh token
        var newRefreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiryDate = _tokenGenerator.GetRefreshTokenExpiration(),
            IsRevoked = false,
            CreatedDate = DateTime.UtcNow
        };

        await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);

        return new AuthResultDto
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = expiresAt,
            Roles = user.Roles.Select(r => r.Name).ToList()
        };
    }
       
    public async Task<bool> RevokeTokenAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (token == null)
            return false;

        token.IsRevoked = true;
        await _refreshTokenRepository.UpdateAsync(token);
        return true;
    }
}
