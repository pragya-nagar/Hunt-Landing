using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Synergy.Common.AspNet;
using Synergy.Common.AspNet.Middleware;
using Synergy.Common.Logging.Configuration;
using Synergy.Common.Security.Extensions;

namespace Synergy.Landing.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            this._hostingEnvironment = env;
            this._configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddCors("synergy");

            services.AddHealthChecks(this._configuration.GetConnectionString("DB"), "Database");

            services.AddSwagger("Synergy.Landing.API");

            services.AddAutoMapper(new Assembly[]
            {
                Assembly.Load("Synergy.Landing.API"),
                Assembly.Load("Synergy.Landing.Domain"),
            });

            this.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, AutoMapper.IMapper mapper)
        {
            app.UseStartupLogging();
            app.UseVersion();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            if (this._hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCorrelationLogging();
            app.UseCors("synergy");
            app.UseHealthCheck();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseOperationContext();
            app.UseMvc();
            app.UseSwagger("Synergy Landing API V1");
        }

        private void RegisterServices(IServiceCollection services)
        {
            var isDevelopment = this._hostingEnvironment.IsDevelopment();

            services.AddDefaultApiContext()
                .AddSerilogLogging(this._configuration)
                .AddAuth(this._configuration, isDevelopment)
                .AddServiceBus()
                .AddFileStorage()
                .AddDomainServices(this._configuration, isDevelopment)
                .AddDomainValidators();
        }
    }
}
