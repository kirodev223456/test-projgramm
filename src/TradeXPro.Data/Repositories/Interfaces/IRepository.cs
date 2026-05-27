using System.Linq.Expressions;

namespace TradeXPro.Data.Repositories.Interfaces;

/// <summary>
/// Generic repository interface. Database provider can be swapped
/// by implementing this interface with a different provider (PostgreSQL, MySQL, etc.)
/// </summary>
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
