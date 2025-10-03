using RabbitMQ.Client;

namespace Rabbit.Message.Api.Messaging
{
    public static class RabbitFactory
    {
        public static IConnection GetConnection(string hostName = "localhost", int port = 5672)
        {
            var factory = new ConnectionFactory() { HostName = hostName, Port = port };
            return factory.CreateConnection(); // <- sincrono, funciona na 6.8.1
        }
    }
}
