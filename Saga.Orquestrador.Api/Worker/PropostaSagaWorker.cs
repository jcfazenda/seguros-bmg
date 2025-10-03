
using RabbitMQ.Client;
using RabbitMQ.Client.Events; 
using System.Text.Json; 
using Infraestructure.Domain.Models;

namespace Saga.Orquestrador.Api.Worker
{
    public class PropostaSagaWorker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "proposta-fila",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("SAGA ouviu a fila!"); // log simples

                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<Propostas>(body);

                if (message != null)
                {

                    // Calcula idade
                    int idade = DateTime.Today.Year - message.DataNascimento.Year;
                    if (message.DataNascimento.Date > DateTime.Today.AddYears(-idade)) 
                        idade--;

                    // Aplica a regra de status
                    message.Status = idade switch
                    {
                        < 18 => "Rejeitada",
                        >= 18 and <= 49 => "Em Análise",
                        >= 50 => "Aprovada"
                    };

                    Console.WriteLine($"Proposta Recebida: {message.NomeCliente} | Status atual: {message.Status}");
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: "proposta-fila", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
