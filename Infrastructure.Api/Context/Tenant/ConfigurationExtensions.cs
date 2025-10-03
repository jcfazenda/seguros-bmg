using System;

namespace Infraestructure.Context.Tenant
{
    public static class ConfigurationExtensions
    {
        public static string GetClientConnectionString(string clientName)
        {
            // Nome do db no formato "prefixo_nomeDB"
            var item = clientName.Split('_');
            if (item.Length < 2)
                throw new ArgumentException("O nome do cliente deve estar no formato 'prefixo_nomeDB'.");

            var dbName = item[0]; // Nome do banco de dados na url (teste)

            // Configurações do Railway
            var server = "mainline.proxy.rlwy.net";
            var port = "40765";
            var user = "root";
            var password = "ZFGhCvQksLBdYdErakuDHhnlMsanEUfz";

            return $"Server={server};Port={port};Database={dbName};Uid={user};Pwd={password};SslMode=Preferred;";
        }
    }
}
