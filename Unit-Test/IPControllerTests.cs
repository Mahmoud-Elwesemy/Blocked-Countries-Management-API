using Xunit;
using Moq;
using Countries.APIs.Controllers;
using Countries.Core.Application.Abstraction.GeoLocation;
using Countries.Core.Application.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class IPControllerTests
{
    private readonly Mock<IGeoLocationService> _geoServiceMock;
    private readonly Mock<IServiceManager> _serviceManagerMock;
    private readonly IPController _controller;

    public IPControllerTests()
    {
        _geoServiceMock = new Mock<IGeoLocationService>();
        _serviceManagerMock = new Mock<IServiceManager>();
        _controller = new IPController(_geoServiceMock.Object, _serviceManagerMock.Object);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    [Fact]
    public async Task Lookup_ReturnsBadRequest_ForInvalidIp()
    {
        var result = await _controller.Lookup("invalid_ip");
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Lookup_ReturnsOk_ForValidIp()
    {
        _geoServiceMock.Setup(x => x.GetCountryCodeAsync("8.8.8.8")).ReturnsAsync("US");
        var result = await _controller.Lookup("8.8.8.8");
        Assert.IsType<OkObjectResult>(result);
    }
}
