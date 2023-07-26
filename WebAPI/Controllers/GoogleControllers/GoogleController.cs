using Application.IServices.GoogleServices;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.GoogleControllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GoogleMapsController : Controller
{
    private readonly IGoogleService _googleService;
    
    public GoogleMapsController(IGoogleService googleService)
    {
        _googleService = googleService;
    }
    
    [HttpGet("/Coordinates/{address}")]
    public async Task<IActionResult> GetCoordinates(string address)
    {
        var coordinates = await _googleService.GetCoordinatesAsync(address);
        return Ok(coordinates);
    }
    
    [HttpGet("/Addresses/{input}")]
    public async Task<IActionResult> GetAddresses(string input)
    {
        var addresses = await _googleService.GetAddressesAsync(input);
        return Ok(addresses);
    }
}