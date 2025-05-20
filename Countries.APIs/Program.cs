
using Countries.Core.Application;
using Countries.Core.Application.Abstraction;
using Countries.Core.Application.Abstraction.GeoLocation;
using Countries.Core.Application.Services.GeoLocationServices;
using Countries.Core.Application.Services.TemporalBlockCleanerServices;
using Countries.Core.Domin.Repositories.contract;
using Countries.Core.Domin.UnitOfWork.Contract;
using Countries.Infrastructure.Presistence.Repositories;
using Countries.Infrastructure.Presistence.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace Countries.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddHttpClient<IGeoLocationService,GeoLocationService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["GeoLocationApi:BaseUrl"]);
            });

            builder.Services.AddScoped(typeof(ICountryRepositories),typeof(CountryRepositories));
            builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(IServiceManager),typeof(ServiceManager));

            builder.Services.AddHostedService<TemporalBlockService>();

            #endregion

            var app = builder.Build();

            #region Configure Middleware
            if(app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json","v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
