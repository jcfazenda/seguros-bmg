using Infraestructure.Domain.Repository.Interface;
using Infraestructure.Domain.Models;
using Microsoft.Extensions.Hosting;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Rabbit.Message.Api.Messaging;

namespace Saga.Orquestrador.Api.Worker
{
    public class PropostaSagaWorker : BackgroundService
    {
        private readonly IRabbitPublisher _publisher;
        private readonly IPropostaRepository _repository;

        public PropostaSagaWorker(IRabbitPublisher publisher, IPropostaRepository repository)
        {
            _publisher = publisher;
            _repository = repository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using var connection = factory.CreateConnection(); // síncrono
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "proposta-fila",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<Propostas>(body);

                if (message != null)
                {
                    message.Status = message.NomeCliente switch
                    {
                        "000.000.000-01" => "Em Análise",
                        "000.000.000-02" => "Rejeitada",
                        _ => "Em Análise"
                    };

                    await _repository.UpdateStatusAsync(message.Id, message.Status);
                    await _publisher.PublishAsync("contrato-fila", message);
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: "proposta-fila", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
