namespace Infraestructure.Domain.Models.Output
{
    public class PropostaOutput
    {
        public Guid Id { get; set; }
        public string NomeCliente { get; set; }= string.Empty;
        public decimal Valor { get; set; }
        public string Status { get; set; }= string.Empty;

    }
}
