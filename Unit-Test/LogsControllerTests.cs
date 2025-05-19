using Xunit;
using Countries.APIs.Controllers;
using Microsoft.AspNetCore.Mvc;

public class LogsControllerTests
{
    [Fact]
    public void GetBlockedAttempts_ReturnsOk()
    {
        var controller = new LogsController();
        var result = controller.GetBlockedAttempts();
        Assert.IsType<OkObjectResult>(result);
    }
}
