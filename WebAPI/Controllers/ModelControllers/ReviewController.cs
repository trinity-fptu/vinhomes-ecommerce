using Application.IServices.Base;
using Domain.Models;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers.ModelControllers;

public class ReviewController : GenericController<Review>
{
    public ReviewController(IGenericService<Review> genericService) : base(genericService)
    {
    }
}