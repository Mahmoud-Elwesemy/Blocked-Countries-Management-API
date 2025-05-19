using Countries.Core.Application.Abstraction.GeoLocation;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Countries.Core.Application.Services.GeoLocationServices;
public class GeoLocationService:IGeoLocationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    public GeoLocationService(HttpClient httpClient,IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    public async Task<string> GetCountryCodeAsync(string ip)
    {
        var apiKey = _config["GeoLocationApi:ApiKey"];
        var response = await _httpClient.GetAsync($"?apiKey={apiKey}&ip={ip}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(content)!;

        return result.country_code2?.ToString()
            ?? result.iso_code?.ToString()
            ?? "Unknown";
    }
}
