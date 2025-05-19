using Countries.Infrastructure.Presistence.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Countries.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LogsController:ControllerBase
{
    [HttpGet("blocked-attempts")]
    public IActionResult GetBlockedAttempts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var logs = Storage.BlockLogs
                .OrderByDescending(log => log.Timestamp) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(logs);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message); 
        }
    }
}
