using Application.IServices.Base;
using Application.ResponseModels;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;
using Infrastructure.Services.GoogleServices;
using WebAPI.CustomMiddleware;

namespace WebAPI.Controllers.ModelControllers;

[GoogleAuthorized(UserRole.Customer)]
public class ProductController : GenericController<Product>
{
    public ProductController(IGenericService<Product> genericService) : base(genericService)
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
