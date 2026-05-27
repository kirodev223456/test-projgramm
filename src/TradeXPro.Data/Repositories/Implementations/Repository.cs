using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TradeXPro.Data.DbContext;
using TradeXPro.Data.Repositories.Interfaces;

namespace TradeXPro.Data.Repositories.Implementations;

/// <summary>
/// Generic repository implementation using EF Core.
/// To swap database: change the DbContext provider registration (SqlServer → Npgsql, etc.)
/// No code changes needed in this class.
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly TradeXProDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(TradeXProDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<bool> ExistsAsync(int id)
        => await _dbSet.FindAsync(id) != null;
}
