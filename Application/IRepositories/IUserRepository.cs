using Application.IRepositories.Base;
using Domain.Models;

namespace Application.IRepositories;

public interface IUserRepository : IGenericRepository<User>
{
}