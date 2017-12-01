using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.Infra.DataAccess;
using EnergyConsumptionMonitor.Infra.Services;

namespace EnergyConsumption
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConsumptionCounterRepository, ConsumptionCounterRepository>();
            services.AddTransient<IExternalAPIService, ExternalAPIService>();
            services.Configure<AppSettings>(Configuration.GetSection("App"));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
