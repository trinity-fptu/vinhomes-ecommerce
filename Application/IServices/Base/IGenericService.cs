using System.Linq.Expressions;
using Application.Commons;

namespace Application.IServices.Base;

public interface IGenericService<T> where T : class
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<T> GetAsync(Guid id, string? include = null);
    Task<IEnumerable<T>> GetAllAsync(string? include);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<Pagination<T>> GetPaginatedAsync(int pageIndex, int pageSize);
    Task<Pagination<T>> GetPaginatedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    Task DeleteRangeAsync(IEnumerable<T> entities);
    Task CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
}