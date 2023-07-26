using Application.IServices.Base;
using Application.ResponseModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers.ModelControllers;

public class OrderController : GenericController<Order>
{
    public OrderController(IGenericService<Order> genericService) : base(genericService)
    {
    }

    // Pagination
    [HttpGet("pagination")]
    public async Task<IActionResult> GetPaginatedAsync(int pageIndex, int pageSize)
    {
        try
        {
            var result = await _genericService.GetAllAsync(x => x.Id == Guid.NewGuid());
            return Ok(new SuccessResponseModel
            {
                Status = Ok().StatusCode,
                Message = "Get Paginated Succeed",
                Result = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(new FailResponseModel
            {
                Status = BadRequest().StatusCode,
                Message = e.Message
            });
        }
    }

    [HttpGet("user/{customerId}")]
    public async Task<IActionResult> GetAll(string? include, Guid customerId)
    {
        var entities = await _genericService.GetAllAsync(include);
        var result = entities.ToList().Where(o => o.CustomerId == customerId);
        return Ok(result);
    }
}