

using Infraestructure.Domain.Models;
using MediatR;

namespace Infraestructure.Domain.Views.Input
{ 
    public class PropostaMadiatorInput : IRequest<Propostas>
    {
        public Guid Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Status { get; set; } = "Em Análise"; 
        public string Cpf { get; set; } = string.Empty; 
        public DateTime DataNascimento { get; set; } 
    }
}