using Infraestructure.Domain.Models;
using MediatR;

namespace Infraestructure.Domain.Services.Commands
{
    public class CriaPropostaCommand : IRequest<Propostas>
    {
        public string NomeCliente { get; set; } = default!;
        public string Cpf { get; set; } = default!;
        public DateTime DataNascimento { get; set; }
        public decimal Valor { get; set; }
        public string? Status { get; set; }
    }
}
