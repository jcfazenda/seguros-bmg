using MediatR;
using Infraestructure.Domain.Models;
using Infraestructure.Domain.Services.Commands;
using Rabbit.Message.Api.Messaging;

namespace Infraestructure.Domain.Services.Handles
{
    public class CreatePropostaHandler : IRequestHandler<CreatePropostaCommand, Guid>
    {
        private readonly IRabbitPublisher _publisher; 

        public CreatePropostaHandler(IRabbitPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreatePropostaCommand request, CancellationToken cancellationToken)
        {
            // Cria o objeto de domínio
            var proposta = new Propostas
            {
                Id = Guid.NewGuid(),
                NomeCliente = request.NomeCliente,
                Valor = request.Valor,
                DataNascimento = request.DataNascimento,
                Cpf = request.Cpf,
                Status = "Em Análise"
            };

            // Publica na fila "proposta-fila"
            await _publisher.PublishAsync("proposta-fila", proposta);

            // Retorna o ID, mesmo sem gravar na base
            return proposta.Id;
        }
    }
}
