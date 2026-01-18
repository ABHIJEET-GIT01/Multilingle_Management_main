using WebAPI_NOVOAssignment.Models;

namespace WebAPI_NOVOAssignment.Repositories.Interfaces;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
    /// <summary>
    /// Gets a user by email
    /// </summary>
    /// <param name="email">User email</param>
    /// <returns>The user or null if not found</returns>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Gets a user by username
    /// </summary>
    /// <param name="username">Username</param>
    /// <returns>The user or null if not found</returns>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>
    /// Gets paginated list of users
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Tuple containing list of users and total count</returns>
    Task<(List<User>, int)> GetPaginatedAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Checks if email exists
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <returns>True if email exists, false otherwise</returns>
    Task<bool> EmailExistsAsync(string email);

    /// <summary>
    /// Checks if username exists
    /// </summary>
    /// <param name="username">Username to check</param>
    /// <returns>True if username exists, false otherwise</returns>
    Task<bool> UsernameExistsAsync(string username);
}
