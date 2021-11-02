namespace Synergy.Landing.API
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using Synergy.Common;
    using Synergy.Common.Abstracts;
    using Synergy.Common.DAL.Abstract;
    using Synergy.Common.DAL.Access.PostgreSQL;
    using Synergy.Common.Security;
    using Synergy.DataAccess.Abstractions;
    using Synergy.Landing.API.Services;
    using Synergy.Landing.Domain;
    using Synergy.Landing.Domain.Abstracts;

    public static class ServiceRegistrations
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddFileStorage(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment = false)
        {
            var connectionString = configuration.GetConnectionString("DB");
            var runMigrations = configuration["DB:RunMigrations"] == "true";

            // for migration in development configuration
            services.RegisterSynergyContext(connectionString, isDevelopment && runMigrations);

            services.AddScoped<IDataAccess>(provider => new DAL.Queries.PostgreSQL.DataAccess(provider.GetService<ILoggerFactory>(), connectionString));

            services.AddTransient(typeof(IQueryProvider<>), typeof(QueryProvider<>));

            services.AddTransient<IDictionaryService, DictionaryService>();

            return services;
        }

        public static IServiceCollection AddDomainValidators(this IServiceCollection services)
        {
            return services;
        }
    }
}