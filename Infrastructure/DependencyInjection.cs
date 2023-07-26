using Application;
using Application.IRepositories.Base;
using Application.IServices.Base;
using Application.IServices.GoogleServices;
using Application.IServices.ModelServices;
using Domain.Models;
using Infrastructure.Repositories.Base;
using Infrastructure.Services.Base;
using Infrastructure.Services.GoogleServices;
using Infrastructure.Services.ModelServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            //options.UseSqlServer(configuration.GetConnectionString("LocalConnection"));
        });

        services.AddScoped<AppDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        services.AddScoped<IGenericService<User>, GenericService<User>>();
        services.AddScoped<IUserService, UserService>();

        var assembly = AppDomain.CurrentDomain.Load("Application");

        // Get all types which implements IGenericRepository<> in your assembly
        var repositories = assembly.GetTypes()
            .Where(x => x is { IsClass: true, IsAbstract: false, IsPublic: true }
                        && x.GetInterfaces().Any(y =>
                            y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IGenericRepository<>)));

        foreach (var repo in repositories)
        {
            foreach (var @interface in repo.GetInterfaces())
            {
                services.AddScoped(@interface, repo);
            }
        }

        // Get all types which implements IService<> in your assembly
        var servicesTypes = assembly.GetTypes()
            .Where(x => x is { IsClass: true, IsAbstract: false, IsPublic: true }
                        && x.GetInterfaces().Any(y =>
                            y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IGenericService<>)));

        //foreach (var service in servicesTypes)
        //{
        //    foreach (var @interface in service.GetInterfaces())
        //    {
        //        services.AddTransient(@interface, service);
        //    }
        //}

        services.AddScoped<IGoogleService, GoogleService>();

        return services;
    }
}