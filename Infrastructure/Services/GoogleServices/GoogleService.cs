using System.Net;
using Application;
using Application.IServices.GoogleServices;
using Application.IServices.ModelServices;
using Domain.Enums;
using Domain.Models;
using Google.Apis.Auth;
using GoogleApi;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Places.AutoComplete.Request;
using GoogleMaps.LocationServices;
using Infrastructure.Configurations.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services.GoogleServices;


public class GoogleService : IGoogleService
{
    // Get Google Maps API Key from appsettings.json using Newtonsoft.Json
    private readonly string _googleMapsApiKey = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("googleservices.json"))!["GoogleMapsApiKey"];
    
    public Task<Dictionary<string, double>> GetCoordinatesAsync(string address)
    {
        var locationService = new GoogleLocationService(_googleMapsApiKey);
        var point = locationService.GetLatLongFromAddress(address);

        var coordinates = new Dictionary<string, double>();

        if (point != null)
        {
            coordinates.Add("Latitude", point.Latitude);
            coordinates.Add("Longitude", point.Longitude);
        }

        return Task.FromResult(coordinates);
    }


    public async Task<IEnumerable<string>> GetAddressesAsync(string input)
    {
        var results = new List<string>();
        var request = new PlacesAutoCompleteRequest
        {
            Key = _googleMapsApiKey,
            Input = input
        };

        var response = await GooglePlaces.AutoComplete.QueryAsync(request);
        if (response.Status != (Status)Directions.Status.OK) return results;
        foreach (var result in response.Predictions)
        {
            results.Add(result.Description);
        }

        return results;
    }

    
}