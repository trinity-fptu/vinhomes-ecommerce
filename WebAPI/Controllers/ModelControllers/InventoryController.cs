using Application.IServices.Base;
using Domain.Models;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers.ModelControllers;

public class InventoryController : GenericController<Inventory>
{
    public InventoryController(IGenericService<Inventory> genericService) : base(genericService)
    {
    }
}