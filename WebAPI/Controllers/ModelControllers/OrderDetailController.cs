using Application.IServices.Base;
using Domain.Models;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers.ModelControllers;

public class OrderDetailController : GenericController<OrderDetail>
{
    public OrderDetailController(IGenericService<OrderDetail> genericService) : base(genericService)
    {
    }
}