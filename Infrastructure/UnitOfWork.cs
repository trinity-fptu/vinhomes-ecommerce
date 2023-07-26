using Application;
using Application.IRepositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<IGenericRepository<T>>();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}