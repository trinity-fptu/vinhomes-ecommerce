using Application.IRepositories;
using Domain.Models;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}