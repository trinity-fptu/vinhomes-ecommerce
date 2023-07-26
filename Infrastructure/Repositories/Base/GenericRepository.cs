using System.Linq.Expressions;
using Application.Commons;
using Application.IRepositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _dbSet = context.Set<T>();
    }
    public async Task Create(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task Update(T model)
    {
        _dbSet.Update(model);
    }

    public async Task Delete(Guid id)
    {
        _dbSet.Remove((await _dbSet.FindAsync(id))!);
    }

    public async Task<T> Get(Guid id, string? include = null)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrEmpty(include))
        {
            // Split the include parameter value into an array of related entity names
            var relatedEntities = include.Split(',');

            // Include the related entities in the query
            foreach (var entityName in relatedEntities)
            {
                query = query.Include(entityName);
            }
        }

        return query.AsEnumerable().FirstOrDefault(entity => entity.GetType().GetProperty("Id").GetValue(entity).ToString().Equals(id.ToString()))!; 
    }

    public async Task<IEnumerable<T>> GetAll(string? include = null)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrEmpty(include))
        {
            // Split the include parameter value into an array of related entity names
            var relatedEntities = include.Split(',');

            // Include the related entities in the query
            foreach (var entityName in relatedEntities)
            {
                query = query.Include(entityName);
            }
        }
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<Pagination<T>> GetPaginated(int pageIndex = 0, int pageSize = 10)
    {
        var count = await _dbSet.CountAsync();
        var items = await _dbSet.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        return new Pagination<T>(items, count, pageIndex, pageSize);
    }

    public async Task<Pagination<T>> GetPaginated(Expression<Func<T, bool>> predicate, int pageIndex = 0, int pageSize = 10)
    {
        var count = await _dbSet.Where(predicate).CountAsync();
        var items = await _dbSet.Where(predicate).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        return new Pagination<T>(items, count, pageIndex, pageSize);
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public Task DeleteRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public async Task Count()
    {
        await _dbSet.CountAsync();
    }

    public async Task<int> Count(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
    }

    public async Task<bool> Any()
    {
        return await _dbSet.AnyAsync();
    }

    public async Task<bool> Any(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
}