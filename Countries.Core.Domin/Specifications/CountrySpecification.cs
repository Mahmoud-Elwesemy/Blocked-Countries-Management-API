using Countries.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Domin.Specifications;
public class CountrySpecification:BaseSpecification<string>
{
    public CountrySpecification(string searchTerm ,int page,int pageSize)
         : base(code =>
            string.IsNullOrEmpty(searchTerm) ||
            code.Contains(searchTerm,StringComparison.OrdinalIgnoreCase))
    {
        ApplyPaging(page,pageSize);
    }
}
