using System;

namespace Infraestructure.Context.Tenant
{
    public static class ConfigurationExtensions
    {
        public static string GetClientConnectionString(string database)
        {  
            var server = "localhost,1433";  
            var user = "SA"; 
            var password = Environment.GetEnvironmentVariable("SA_PASSWORD") ?? "proposta@56!";

            return $"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate=True;";
        }
    }
}

