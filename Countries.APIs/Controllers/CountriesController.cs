using Countries.Core.Application.Abstraction;
using Countries.Core.Domin.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Countries.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CountriesController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CountriesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("block")]
    public async Task<IActionResult> BlockCountry([FromBody] string countryCode)
    {
        try
        {
            await _serviceManager.CountriesService.BlockCountryAsync(countryCode);
            return Ok();
        }
        catch(InvalidOperationException ex)
        {
            return Conflict(ex.Message); 
        }
    }

    [HttpDelete("block/{countryCode}")]
    public async Task<IActionResult> UnblockCountry(string countryCode)
    {
        try
        {
            await _serviceManager.CountriesService.UnblockCountryAsync(countryCode);
            return Ok();
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message); 
        }
    }

   
    [HttpGet("blocked")]
    public async Task<IActionResult> GetBlockedCountries(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string searchTerm = "")
    {
        try
        {
            var countries = await _serviceManager.CountriesService.GetBlockedCountriesAsync(page,pageSize,searchTerm);
            return Ok(countries);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message); 
        }
    }

    [HttpPost("temporal-block")]
    public async Task<IActionResult> TemporarilyBlockCountry([FromBody] TemporalBlockRequest request)
    {
        try
        {
            await _serviceManager.CountriesService.TemporarilyBlockCountryAsync(request.CountryCode,request.DurationMinutes);
            return Ok();
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message); 
        }
        catch(InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
