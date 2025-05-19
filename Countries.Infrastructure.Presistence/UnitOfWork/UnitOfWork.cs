using Countries.Core.Domin.Repositories.contract;
using Countries.Core.Domin.UnitOfWork.Contract;
using Countries.Infrastructure.Presistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Presistence.UnitOfWork;
public class UnitOfWork:IUnitOfWork
{
    private readonly ConcurrentDictionary<string,object> _repositories;
    private bool _disposed;

    public UnitOfWork()
    {
        _repositories = new ConcurrentDictionary<string,object>();
    }
    public ICountryRepositories GetRepositories()
    {
        return (ICountryRepositories) _repositories.GetOrAdd(
             typeof(CountryRepositories).Name,
             _ => new CountryRepositories()
         );
    }
    public Task<int> CompleteAsync()
    {
        return Task.FromResult(0);
    }

    public async ValueTask DisposeAsync()
    {
        if(!_disposed)
        {
            _disposed = true;
            await Task.CompletedTask;
        }
    }
}