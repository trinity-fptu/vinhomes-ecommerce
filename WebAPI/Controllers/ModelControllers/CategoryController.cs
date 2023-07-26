using Application.IServices.Base;
using Domain.Models;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers.ModelControllers;

public class CategoryController : GenericController<Category>
{
    public CategoryController(IGenericService<Category> genericService) : base(genericService)
    {
    }
}