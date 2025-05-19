using Countries.Core.Application.Abstraction.GeoLocation;
using Countries.Core.Application.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Countries.Core.Domin.Entities;
using Countries.Infrastructure.Presistence.Data;
using System.Net;

namespace Countries.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IPController:ControllerBase
{
    private readonly IGeoLocationService _geoService;
    private readonly IServiceManager _serviceManager;

    public IPController(IGeoLocationService geoService,IServiceManager serviceManager)
    {
        _geoService = geoService;
        _serviceManager = serviceManager;
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> Lookup([FromQuery] string ipAddress)
    {
        try
        {
            if(!IPAddress.TryParse(ipAddress,out _))
                return BadRequest("Invalid IP format");

            var countryCode = await _geoService.GetCountryCodeAsync(ipAddress);
            return Ok(new { CountryCode = countryCode });
        }
        catch(Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }


    [HttpGet("check-block")]
    public async Task<IActionResult> CheckBlock()
    {

        try
        {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            if(string.IsNullOrEmpty(ip))
            {
                return BadRequest("IP address not found");
            }
            if(IsBogonIp(ip))
            {
                ip = "45.242.225.63"; // IP افتراضي
            }
            var countryCode = await _geoService.GetCountryCodeAsync(ip);

            bool isPermanentlyBlocked = Storage.BlockedCountries.ContainsKey(countryCode);
            bool isTemporarilyBlocked = Storage.TemporalBlocks.TryGetValue(countryCode,out var expiration)
                                        && expiration > DateTime.UtcNow;

            bool isBlocked = isPermanentlyBlocked || isTemporarilyBlocked;

            Storage.BlockLogs.Add(new BlockLog
            {
                IpAddress = ip,
                CountryCode = countryCode,
                Timestamp = DateTime.UtcNow,
                UserAgent = HttpContext.Request.Headers.UserAgent.ToString(),
                IsBlocked = isBlocked
            });

            return Ok(new { IsBlocked = isBlocked });
        }
        catch(Exception ex)
        {
            return StatusCode(500,$"Internal server error: {ex.Message}");
        }
    }

    private bool IsBogonIp(string ip)
    {
        var bogonIps = new[] { "::1","127.0.0.1","0:0:0:0:0:0:0:1" };
        return bogonIps.Contains(ip);
    }
}