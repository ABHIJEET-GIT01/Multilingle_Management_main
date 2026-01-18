using WebAPI_NOVOAssignment.Models;

namespace WebAPI_NOVOAssignment.Repositories.Interfaces;

/// <summary>
/// Repository interface for RefreshToken entity operations
/// </summary>
public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    /// <summary>
    /// Gets a refresh token by token value
    /// </summary>
    /// <param name="token">Token value</param>
    /// <returns>The refresh token or null if not found</returns>
    Task<RefreshToken?> GetByTokenAsync(string token);

    /// <summary>
    /// Gets all refresh tokens for a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of refresh tokens</returns>
    Task<List<RefreshToken>> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Revokes a refresh token
    /// </summary>
    /// <param name="tokenId">Token ID to revoke</param>
    /// <returns>True if revoked successfully, false if not found</returns>
    Task<bool> RevokeTokenAsync(Guid tokenId);

    /// <summary>
    /// Removes expired tokens
    /// </summary>
    /// <returns>Number of tokens removed</returns>
    Task<int> RemoveExpiredTokensAsync();
}
