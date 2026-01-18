using WebAPI_NOVOAssignment.DTOs;

namespace WebAPI_NOVOAssignment.Services.Interfaces;

/// <summary>
/// Service interface for authentication operations
/// </summary>
public interface IAuthService
{
    Task<AuthResultDto?> LoginAsync(LoginRequestDto request);
    Task<AuthResultDto?> RefreshTokenAsync(string refreshToken);
    Task<bool> RevokeTokenAsync(string refreshToken);
}
