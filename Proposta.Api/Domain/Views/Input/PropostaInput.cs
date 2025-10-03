namespace Proposta.Domain.Models.Input
{
    public class PropostaInput
    {
        public string NomeCliente { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public decimal Valor { get; set; }
    }
}
