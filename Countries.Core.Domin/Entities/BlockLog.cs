using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Domin.Entities;
public class BlockLog
{
    public string IpAddress { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string CountryCode { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
}
