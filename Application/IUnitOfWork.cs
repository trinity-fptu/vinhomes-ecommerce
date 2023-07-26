using Application.IRepositories.Base;

namespace Application;

public interface IUnitOfWork
{
    IGenericRepository<T> GetRepository<T>() where T : class;
    Task<int> SaveChangesAsync();
}