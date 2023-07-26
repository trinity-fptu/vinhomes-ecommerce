using Application.IRepositories;
using Domain.Models;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
    }
}