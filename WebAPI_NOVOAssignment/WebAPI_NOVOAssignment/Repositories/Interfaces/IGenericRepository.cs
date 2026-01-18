namespace WebAPI_NOVOAssignment.Repositories.Interfaces;

/// <summary>
/// Generic repository interface for common CRUD operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Gets all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    Task<List<T>> GetAllAsync();

    /// <summary>
    /// Gets an entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>The entity or null if not found</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Adds a new entity
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <returns>The added entity</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="entity">Entity to update</param>
    /// <returns>The updated entity</returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="id">ID of entity to delete</param>
    /// <returns>True if deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Checks if an entity exists
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>True if exists, false otherwise</returns>
    Task<bool> ExistsAsync(Guid id);
}
