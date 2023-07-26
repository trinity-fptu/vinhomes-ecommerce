using Application.IRepositories;
using Domain.Models;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }
}