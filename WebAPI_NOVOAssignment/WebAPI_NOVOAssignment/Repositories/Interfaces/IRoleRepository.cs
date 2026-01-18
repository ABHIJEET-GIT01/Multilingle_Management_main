using WebAPI_NOVOAssignment.Models;

namespace WebAPI_NOVOAssignment.Repositories.Interfaces;

/// <summary>
/// Repository interface for Role entity operations
/// </summary>
public interface IRoleRepository : IGenericRepository<Role>
{
    /// <summary>
    /// Gets a role by name
    /// </summary>
    /// <param name="name">Role name</param>
    /// <returns>The role or null if not found</returns>
    Task<Role?> GetByNameAsync(string name);

    /// <summary>
    /// Gets paginated list of roles
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Tuple containing list of roles and total count</returns>
    Task<(List<Role>, int)> GetPaginatedAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Checks if role name exists
    /// </summary>
    /// <param name="name">Role name to check</param>
    /// <returns>True if role name exists, false otherwise</returns>
    Task<bool> RoleNameExistsAsync(string name);
}
