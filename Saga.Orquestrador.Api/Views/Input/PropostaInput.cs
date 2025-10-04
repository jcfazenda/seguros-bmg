namespace Saga.Orquestrador.Api.Views.Input
{
    public class PropostaInput
    {
        public Guid Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Status { get; set; } = "Em Análise"; 
        public string Cpf { get; set; } = string.Empty; 
        public DateTime DataNascimento { get; set; }

    }
}
