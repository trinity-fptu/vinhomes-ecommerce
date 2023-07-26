using Application.IRepositories;
using Domain.Models;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}