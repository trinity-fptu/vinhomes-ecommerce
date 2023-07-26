using Application.IRepositories;
using Domain.Models;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(AppDbContext context) : base(context)
    {
    }
}