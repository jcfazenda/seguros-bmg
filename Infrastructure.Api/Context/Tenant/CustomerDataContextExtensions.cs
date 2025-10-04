using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

                // Busca a connection string dinâmica (SQL Server)
                var connString = ConfigurationExtensions.GetClientConnectionString(clientSlug);

                // Configura o DbContext para SQL Server
                var optionsBuilder = new DbContextOptionsBuilder<SeguroContext>();
                optionsBuilder.UseSqlServer(
                    connString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.EnableRetryOnFailure(); // retries automáticos
                        sqlServerOptions.MigrationsAssembly(typeof(SeguroContext).Assembly.FullName);
                    }
                );

                // Logs sensíveis em DEV
                optionsBuilder.EnableSensitiveDataLogging();

                return new SeguroContext(optionsBuilder.Options);
            });
        }

        
    }

    
}
