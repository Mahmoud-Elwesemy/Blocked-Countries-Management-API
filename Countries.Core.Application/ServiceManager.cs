using Countries.Core.Application.Abstraction;
using Countries.Core.Application.Abstraction.Countries;
using Countries.Core.Application.Abstraction.GeoLocation;
using Countries.Core.Application.Services.CountriesServices;
using Countries.Core.Application.Services.GeoLocationServices;
using Countries.Core.Domin.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Application;
public class ServiceManager:IServiceManager
{
    // Lazy loading of services to improve performance
    // This allows the services to be created only when they are accessed for the first time.
    // This can help reduce the startup time of the application and improve overall performance.
    // It also helps to avoid unnecessary instantiation of services that may not be used.
    // This is particularly useful in scenarios where the services are expensive to create or have dependencies that may not be needed immediately.
    // By using Lazy<T>, the services are created only when they are accessed, which can help improve performance.
    // Lazy<T> is a thread-safe way to create objects only when they are needed.
    // This can help improve performance by avoiding unnecessary instantiation of services that may not be used.

    private readonly Lazy<ICountriesService> _countriesService;
    public ServiceManager(IUnitOfWork unitOfWork)
    {
        // Initialize the services using Lazy<T> to defer their creation until they are accessed 
        _countriesService = new Lazy<ICountriesService>(() => new CountriesService(unitOfWork));
    }
    // Properties to access the services
    public ICountriesService CountriesService => _countriesService.Value;

    public IGeoLocationService GeoLocationService => throw new NotImplementedException();
}
