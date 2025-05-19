using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Domin.Specifications;
public interface ISpecification<T>
{
    Expression<Func<T,bool>> Criteria { get; }
    int? Skip { get; }
    int? Take { get; }
    bool IsPagingEnabled { get; }
}
