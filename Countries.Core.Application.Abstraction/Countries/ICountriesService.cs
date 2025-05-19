using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Application.Abstraction.Countries;
public interface ICountriesService
{
    Task BlockCountryAsync(string countryCode);
    Task UnblockCountryAsync(string countryCode);
    Task<IEnumerable<string>> GetBlockedCountriesAsync(int page,int pageSize,string searchTerm);
    Task<bool> IsCountryBlockedAsync(string countryCode);
    Task TemporarilyBlockCountryAsync(string countryCode,int durationMinutes);
}

