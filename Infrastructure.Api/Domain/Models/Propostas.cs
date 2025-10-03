namespace Infraestructure.Domain.Models
{
    public class Propostas
    {
        public Propostas() { }
        
        public Propostas(Guid Id , string NomeCliente, decimal Valor, string Status)
        {
            this.Id = Id;
            this.NomeCliente = NomeCliente;
            this.Valor = Valor;
            this.Status = Status;
        }

        public Guid Id { get; set; }
        public string NomeCliente { get; set; }= string.Empty;
        public decimal Valor { get; set; }
        public string Status { get; set; } = "Em Análise";

    }
}
