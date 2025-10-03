using MediatR;
using Infraestructure.Domain.Models.Output;

namespace Infraestructure.Domain.Services.Commands
{
    public record CreatePropostaCommand(string NomeCliente, decimal Valor, DateTime DataNascimento, string Cpf) : IRequest<Guid>;
}
