using Application.IServices.Base;
using Domain.Models;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers.ModelControllers;

public class PaymentController : GenericController<Payment>
{
    public PaymentController(IGenericService<Payment> genericService) : base(genericService)
    {
    }
}