using Microsoft.EntityFrameworkCore;
using WebAPI_NOVOAssignment.Data;

namespace WebAPI_NOVOAssignment.Repositories.Interfaces;

/// <summary>
/// Generic repository implementation for common CRUD operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    /// <summary>
    /// Initializes a new instance of the GenericRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

   
    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

   
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

   
    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

   
    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

   
    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

   
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) != null;
    }
}
