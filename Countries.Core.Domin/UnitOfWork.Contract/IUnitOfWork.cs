using Countries.Core.Domin.Repositories.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Domin.UnitOfWork.Contract;
// This Is A Unit Of Work Interface That Contains The Repositories
public interface IUnitOfWork:IAsyncDisposable
{
    // This Is A Method That Get The Generic Repository And All Repositories If Found
    ICountryRepositories GetRepositories();
    Task<int> CompleteAsync();
}
