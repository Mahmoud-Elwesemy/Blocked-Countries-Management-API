using Countries.Core.Domin.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Presistence.Data;
public static class Storage
{
    public static ConcurrentDictionary<string,DateTime> BlockedCountries = new();
    public static ConcurrentBag<BlockLog> BlockLogs = new();
    public static ConcurrentDictionary<string,DateTime> TemporalBlocks = new();
}
