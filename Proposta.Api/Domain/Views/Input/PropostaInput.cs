namespace Proposta.Domain.Viewa.Input
{
    public class PropostaInput
    {
        public string NomeCliente { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public decimal Valor { get; set; }
    }
}
