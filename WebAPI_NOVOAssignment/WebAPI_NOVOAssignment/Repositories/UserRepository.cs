using Microsoft.EntityFrameworkCore;
using WebAPI_NOVOAssignment.Data;
using WebAPI_NOVOAssignment.Models;
using WebAPI_NOVOAssignment.Repositories.Interfaces;

namespace WebAPI_NOVOAssignment.Repositories;

/// <summary>
/// Repository implementation for User entity operations
/// </summary>
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
 
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

     public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Username == username);
    }
     
    public async Task<(List<User>, int)> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        var query = _context.Users.Include(u => u.Roles);
        var totalCount = await query.CountAsync();
        var users = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, totalCount);
    }

     public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
     
    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }
}
