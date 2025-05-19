using Countries.Core.Domin.Repositories.contract;
using Countries.Core.Domin.Specifications;
using Countries.Infrastructure.Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Presistence.Repositories;
public class CountryRepositories:ICountryRepositories
{
    public Task<IEnumerable<string>> GetAsync(ISpecification<string> spec)
    {
        try
        {
            var query = Storage.BlockedCountries.Keys.AsQueryable();

            if(spec.Criteria != null)
                query = query.Where(spec.Criteria);

            if(spec.IsPagingEnabled)
                query = query.Skip(spec.Skip.Value).Take(spec.Take.Value);

            return Task.FromResult(query.AsEnumerable());
        }
        catch(Exception ex)
        {      
            throw new ApplicationException("Error retrieving countries",ex);
        }
    }

    public Task AddAsync(string countryCode)
    {
        if(!Storage.BlockedCountries.TryAdd(countryCode,DateTime.UtcNow))
            throw new InvalidOperationException("Country already exists");
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string countryCode)
    {
        return Task.FromResult(Storage.BlockedCountries.ContainsKey(countryCode));
    }

    public Task RemoveAsync(string countryCode)
    {
        if(!Storage.BlockedCountries.TryRemove(countryCode,out _))
            throw new KeyNotFoundException("Country not found");

        return Task.CompletedTask;
    }

    public Task TemporarilyBlockAsync(string countryCode,DateTime expiration)
    {
        if(Storage.TemporalBlocks.ContainsKey(countryCode))
            throw new InvalidOperationException("Country already temporarily blocked");
        if(Storage.BlockedCountries.ContainsKey(countryCode))
            throw new InvalidOperationException("Country already blocked");
        Storage.TemporalBlocks.TryAdd(countryCode,expiration);
        return Task.CompletedTask;
    }
}
