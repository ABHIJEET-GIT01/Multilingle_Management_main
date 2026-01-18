using Microsoft.EntityFrameworkCore;
using WebAPI_NOVOAssignment.Data;
using WebAPI_NOVOAssignment.Models;
using WebAPI_NOVOAssignment.Repositories.Interfaces;

namespace WebAPI_NOVOAssignment.Repositories;

/// <summary>
/// Repository implementation for RefreshToken entity operations
/// </summary>
public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the RefreshTokenRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public RefreshTokenRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<List<RefreshToken>> GetByUserIdAsync(Guid userId)
    {
        return await _context.RefreshTokens
            .Where(rt => rt.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> RevokeTokenAsync(Guid tokenId)
    {
        var token = await GetByIdAsync(tokenId);
        if (token == null)
            return false;

        token.IsRevoked = true;
        await UpdateAsync(token);
        return true;
    }

    public async Task<int> RemoveExpiredTokensAsync()
    {
        var expiredTokens = await _context.RefreshTokens
            .Where(rt => rt.ExpiryDate < DateTime.UtcNow)
            .ToListAsync();

        _context.RefreshTokens.RemoveRange(expiredTokens);
        await _context.SaveChangesAsync();
        return expiredTokens.Count;
    }
}
