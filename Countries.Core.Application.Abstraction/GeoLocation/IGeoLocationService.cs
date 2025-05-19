using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Application.Abstraction.GeoLocation;
public interface IGeoLocationService
{
    Task<string> GetCountryCodeAsync(string ip);
}
