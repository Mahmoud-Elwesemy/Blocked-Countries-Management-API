using Countries.Core.Application.Abstraction.Countries;
using Countries.Core.Domin.Specifications;
using Countries.Core.Domin.UnitOfWork.Contract;
using Countries.Infrastructure.Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Application.Services.CountriesServices;
internal class CountriesService:ICountriesService
{
    private readonly IUnitOfWork _unitOfWork;

    public CountriesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<string>> GetBlockedCountriesAsync(int page,int pageSize,string searchTerm)
    {
        var spec = new CountrySpecification(searchTerm,page,pageSize);
        return await _unitOfWork.GetRepositories().GetAsync(spec);
    }
    public async Task BlockCountryAsync(string countryCode)
    {
        var repository = _unitOfWork.GetRepositories();
        if(await repository.ExistsAsync(countryCode))
            throw new InvalidOperationException("Country already blocked");

        await repository.AddAsync(countryCode);
        await _unitOfWork.CompleteAsync();
    }


    public async Task<bool> IsCountryBlockedAsync(string countryCode)
    {
        return await _unitOfWork.GetRepositories().ExistsAsync(countryCode);
    }

    public async Task UnblockCountryAsync(string countryCode)
    {
        var repository = _unitOfWork.GetRepositories();
        if(!await repository.ExistsAsync(countryCode))
            throw new KeyNotFoundException("Country not found");

        await repository.RemoveAsync(countryCode);
        await _unitOfWork.CompleteAsync();
    }

    public async Task TemporarilyBlockCountryAsync(string countryCode,int durationMinutes)
    {
        if(durationMinutes < 1 || durationMinutes > 1440)
            throw new ArgumentException("Invalid duration");

        var expiration = DateTime.UtcNow.AddMinutes(durationMinutes);
        var repository = _unitOfWork.GetRepositories();

        await repository.TemporarilyBlockAsync(countryCode,expiration);
        await _unitOfWork.CompleteAsync();
    }
}
