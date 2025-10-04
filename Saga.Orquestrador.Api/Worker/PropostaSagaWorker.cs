using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json; 
using Saga.Orquestrador.Api.Processor;
using Rabbit.Message.Api.Messaging;
using Saga.Orquestrador.Api.Views.Input;

namespace Saga.Orquestrador.Api.Worker
{
    public class PropostaSagaWorker : BackgroundService
    {
        private readonly IRabbitPublisher _publisher;
        private readonly IServiceProvider _serviceProvider;

        public PropostaSagaWorker(IRabbitPublisher publisher, IServiceProvider serviceProvider)
        {
            _publisher = publisher;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "proposta-fila",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageInput = JsonSerializer.Deserialize<PropostaInput>(body);

                if (messageInput != null)
                {
                    // Processa a proposta
                    var proposta = PropostaProcessor.Processar(messageInput);
                    await _publisher.PublishAsync("contrato-fila", proposta);

                    Console.WriteLine($"Proposta processada: {proposta.NomeCliente}, Status: {proposta.Status}");
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: "proposta-fila", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
