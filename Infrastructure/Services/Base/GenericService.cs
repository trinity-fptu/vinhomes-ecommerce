using System.Linq.Expressions;
using Application;
using Application.Commons;
using Application.IServices.Base;

namespace Infrastructure.Services.Base;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GenericService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task CreateAsync(T entity)
    {
        await _unitOfWork.GetRepository<T>().Create(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        await _unitOfWork.GetRepository<T>().Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.GetRepository<T>().Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<T> GetAsync(Guid id, string? include = null)
    {
        return await _unitOfWork.GetRepository<T>().Get(id, include);
    }

    public async Task<IEnumerable<T>> GetAllAsync(string? include)
    {
        return await _unitOfWork.GetRepository<T>().GetAll(include);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _unitOfWork.GetRepository<T>().GetAll(predicate);
    }

    public async Task<Pagination<T>> GetPaginatedAsync(int pageIndex, int pageSize)
    {
        return await _unitOfWork.GetRepository<T>().GetPaginated(pageIndex, pageSize);
    }

    public async Task<Pagination<T>> GetPaginatedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
    {
        return await _unitOfWork.GetRepository<T>().GetPaginated(predicate, pageIndex, pageSize);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _unitOfWork.GetRepository<T>().AddRange(entities);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        await _unitOfWork.GetRepository<T>().UpdateRange(entities);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        await _unitOfWork.GetRepository<T>().DeleteRange(entities);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CountAsync()
    {
        await _unitOfWork.GetRepository<T>().Count();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _unitOfWork.GetRepository<T>().Count(predicate);
    }

    public async Task<bool> AnyAsync()
    {
        return await _unitOfWork.GetRepository<T>().Any();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _unitOfWork.GetRepository<T>().Any(predicate);
    }
}