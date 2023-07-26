using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.IServices.GoogleServices;

public interface IGoogleService
{
    Task<Dictionary<string, double>> GetCoordinatesAsync(string address);
    Task<IEnumerable<string>> GetAddressesAsync(string input);
}