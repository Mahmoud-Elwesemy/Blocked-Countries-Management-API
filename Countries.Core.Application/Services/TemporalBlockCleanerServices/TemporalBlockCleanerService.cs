using Countries.Core.Domin.UnitOfWork.Contract;
using Countries.Infrastructure.Presistence.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Application.Services.TemporalBlockCleanerServices;
public class TemporalBlockService:BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            foreach(var entry in Storage.TemporalBlocks)
            {
                if(DateTime.UtcNow >= entry.Value)
                    Storage.TemporalBlocks.TryRemove(entry.Key,out _);
            }
            await Task.Delay(TimeSpan.FromMinutes(5),stoppingToken);
        }
    }
}

