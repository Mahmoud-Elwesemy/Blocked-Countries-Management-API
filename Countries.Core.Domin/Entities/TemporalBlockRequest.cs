using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Domin.Entities;
public class TemporalBlockRequest
{
    public string CountryCode { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }

}
