using Microsoft.EntityFrameworkCore;
using WebAPI_NOVOAssignment.Data;
using WebAPI_NOVOAssignment.Models;
using WebAPI_NOVOAssignment.Repositories.Interfaces;

namespace WebAPI_NOVOAssignment.Repositories;

/// <summary>
/// Repository implementation for Role entity operations
/// </summary>
public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the RoleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
  
    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name);
    }
  
    public async Task<(List<Role>, int)> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        var query = _context.Roles;
        var totalCount = await query.CountAsync();
        var roles = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (roles, totalCount);
    }
  
    public async Task<bool> RoleNameExistsAsync(string name)
    {
        return await _context.Roles.AnyAsync(r => r.Name == name);
    }
}
