using RabbitMQ.Client;

namespace Rabbit.Message.Api.Messaging
{
    public static class RabbitFactory
    {
        public static IConnection GetConnection(string hostName = "localhost", int port = 5672)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = hostName, 
                Port = port,
                DispatchConsumersAsync = true // importante se você for usar AsyncEventingBasicConsumer
            };
            return factory.CreateConnection();
        }
    }
}
