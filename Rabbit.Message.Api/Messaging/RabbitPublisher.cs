using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Rabbit.Message.Api.Messaging
{
    public class RabbitPublisher : IRabbitPublisher, IDisposable
    {
        private readonly IConnection _connection;

        public RabbitPublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            _connection = factory.CreateConnection(); // síncrono
        }

        public Task PublishAsync<T>(string queueName, T message)
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
