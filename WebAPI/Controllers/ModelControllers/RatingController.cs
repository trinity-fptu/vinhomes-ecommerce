using Application.IServices.Base;
using Domain.Models;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers.ModelControllers;

public class RatingController : GenericController<Rating>
{
    public RatingController(IGenericService<Rating> genericService) : base(genericService)
    {
    }
}