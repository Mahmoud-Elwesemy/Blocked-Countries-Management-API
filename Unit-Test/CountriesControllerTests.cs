using Xunit;
using Moq;
using Countries.APIs.Controllers;
using Countries.Core.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Countries.Core.Application.Abstraction.Countries;

public class CountriesControllerTests
{
    private readonly Mock<IServiceManager> _serviceManagerMock;
    private readonly Mock<ICountriesService> _countriesServiceMock;
    private readonly CountriesController _controller;

    public CountriesControllerTests()
    {
        _serviceManagerMock = new Mock<IServiceManager>();
        _countriesServiceMock = new Mock<ICountriesService>();
        _serviceManagerMock.SetupGet(x => x.CountriesService).Returns(_countriesServiceMock.Object);
        _controller = new CountriesController(_serviceManagerMock.Object);
    }

    [Fact]
    public async Task BlockCountry_ReturnsOk()
    {
        _countriesServiceMock.Setup(x => x.BlockCountryAsync("US")).Returns(Task.CompletedTask);
        var result = await _controller.BlockCountry("US");
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UnblockCountry_ReturnsOk()
    {
        _countriesServiceMock.Setup(x => x.UnblockCountryAsync("US")).Returns(Task.CompletedTask);
        var result = await _controller.UnblockCountry("US");
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task GetBlockedCountries_ReturnsOk()
    {
        _countriesServiceMock.Setup(x => x.GetBlockedCountriesAsync(1, 10, ""))
            .ReturnsAsync(new List<string>());
        var result = await _controller.GetBlockedCountries();
        Assert.IsType<OkObjectResult>(result);
    }
}
