using Application.IServices.Base;
using Application.ResponseModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers.ModelControllers;

public class StoreController : GenericController<Store>
{
    public StoreController(IGenericService<Store> genericService) : base(genericService)
    {
    }
    
    // Pagination
    [HttpGet("pagination")]
    public async Task<IActionResult> GetPaginatedAsync(int pageIndex, int pageSize)
    {
        try
        {
            var result = await _genericService.GetPaginatedAsync(pageIndex, pageSize);
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
}