using Application.IServices.Base;
using Application.ResponseModels;
using Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.CustomMiddleware;

namespace WebAPI.Controllers.Base;

[GoogleAuthorized(UserRole.Customer)]
[ApiController]
[Route("api/v1/[controller]")]
public class GenericController<T> : Controller where T : class
{
    protected readonly IGenericService<T> _genericService;

    public GenericController(IGenericService<T> genericService)
    {
        _genericService = genericService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(T entity)
    {
        await _genericService.CreateAsync(entity);
        return Ok(new SuccessResponseModel
        {
            Status = Ok().StatusCode,
            Message = ""
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, string? include = null)
    {
        var entity = await _genericService.GetAsync(id, include);
        if(entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string? include)
    {
        var entities = await _genericService.GetAllAsync(include);
        return Ok(entities);
    }

    [HttpGet("paginate")]
    public async Task<IActionResult> GetPaginated(int pageIndex, int pageSize)
    {
        var paginatedEntities = await _genericService.GetPaginatedAsync(pageIndex, pageSize);
        return Ok(paginatedEntities);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, T entity)
    {
        var idProperty = entity.GetType().GetProperty("Id");

        if (idProperty != null && idProperty.PropertyType == typeof(Guid))
        {
            // Get the value of the 'Id' property
            var idValue = (Guid)idProperty.GetValue(entity);

            if (id.Equals(idValue))
            {
                await _genericService.UpdateAsync(entity);
                return Ok();
            }
        }
        return BadRequest("Wrong id");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _genericService.DeleteAsync(id);
        return NoContent();
    }
}
