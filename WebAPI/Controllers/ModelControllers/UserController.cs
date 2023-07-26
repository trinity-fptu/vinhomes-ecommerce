using Application.IServices.Base;
using Domain.Models;
using WebAPI.Controllers.Base;
using WebAPI.CustomMiddleware;

namespace WebAPI.Controllers.ModelControllers;

public class UserController : GenericController<User>
{
    private readonly FirebaseAuthentication _authService;

    public UserController(IGenericService<User> genericService, FirebaseAuthentication authService) 
        : base(genericService)
    {
        _authService = authService;
    }

    // other methods...
}