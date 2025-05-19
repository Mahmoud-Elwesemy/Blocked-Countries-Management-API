using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Domin.Specifications;
public class BaseSpecification<T>:ISpecification<T>
{
    public Expression<Func<T,bool>> Criteria { get; }
    public int? Skip { get; private set; }
    public int? Take { get; private set; }
    public bool IsPagingEnabled { get; private set; } = false;
    public BaseSpecification(Expression<Func<T,bool>> criteria)
    {
        Criteria = criteria;
    }

    public void ApplyPaging(int page,int pageSize)
    {
        if(page < 1 || pageSize < 1)
            throw new ArgumentException("Invalid paging parameters");
        Skip = (page - 1) * pageSize;
        Take = pageSize;
        IsPagingEnabled = true;
    }
}
