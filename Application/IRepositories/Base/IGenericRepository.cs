using System.Linq.Expressions;
using Application.Commons;

namespace Application.IRepositories.Base;

public interface IGenericRepository<T> where T : class
{
    // CRUD operations
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(Guid id);
    Task<T> Get(Guid id, string? include = null);
    Task<IEnumerable<T>> GetAll(string? include);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

    // Pagination
    Task<Pagination<T>> GetPaginated(int pageIndex = 0, int pageSize = 10);
    Task<Pagination<T>> GetPaginated(Expression<Func<T, bool>> predicate, int pageIndex = 0, int pageSize = 10);

    // Bulk operations
    Task AddRange(IEnumerable<T> entities);
    Task UpdateRange(IEnumerable<T> entities);
    Task DeleteRange(IEnumerable<T> entities);

    // Count
    Task Count();
    Task<int> Count(Expression<Func<T, bool>> predicate);

    // Any
    Task<bool> Any();
    Task<bool> Any(Expression<Func<T, bool>> predicate);
}