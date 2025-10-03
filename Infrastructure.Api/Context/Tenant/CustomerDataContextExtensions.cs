using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Infraestructure.Context;

namespace Infraestructure.Context.Tenant
{
    public static class CustomerDataContextExtensions
    {
        public static void AddCustomerDbContext(this IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var httpContextAccessor = provider.GetService<IHttpContextAccessor>();

                var path = httpContextAccessor?.HttpContext?.Request?.Path.Value;
                if (string.IsNullOrEmpty(path))
                    throw new InvalidOperationException("HttpContext ou Path não disponível.");

                // Recupera o primeiro segmento da URL (ex: /meudb/api/... -> meudb)
                var clientSlug = path.Split("/", StringSplitOptions.RemoveEmptyEntries)[0];

                // Busca a connection string dinâmica (MySQL)
                var connString = ConfigurationExtensions.GetClientConnectionString(clientSlug);

                // Configura o DbContext para MySQL
                var optionsBuilder = new DbContextOptionsBuilder<SeguroContext>();
                optionsBuilder.UseMySql(
                    connString,
                    ServerVersion.AutoDetect(connString), 
                    mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(); 
                        mySqlOptions.MigrationsAssembly(typeof(SeguroContext).Assembly.FullName);
                    }
                );

                // Logs sensíveis em DEV
                optionsBuilder.EnableSensitiveDataLogging();

                return new SeguroContext(optionsBuilder.Options);
            });
        }
    }
}
