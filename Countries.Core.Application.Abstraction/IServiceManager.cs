using Countries.Core.Application.Abstraction.Countries;
using Countries.Core.Application.Abstraction.GeoLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Application.Abstraction;
public interface IServiceManager
{
    // Define all the services that the service manager will provide
    ICountriesService CountriesService { get; }
    IGeoLocationService GeoLocationService { get; }
}
