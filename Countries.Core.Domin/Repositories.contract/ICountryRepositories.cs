using Countries.Core.Domin.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Domin.Repositories.contract;
public interface ICountryRepositories
{
    Task AddAsync(string countryCode);
    Task RemoveAsync(string countryCode);
    Task<bool> ExistsAsync(string countryCode);
    Task<IEnumerable<string>> GetAsync(ISpecification<string> spec);
    Task TemporarilyBlockAsync(string countryCode,DateTime expiration);
}
