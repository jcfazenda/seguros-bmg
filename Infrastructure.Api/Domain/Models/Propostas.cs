namespace Infraestructure.Domain.Models
{
    public class Propostas
    {
        public Propostas() { }

        public Propostas(Guid Id, string NomeCliente, decimal Valor, string Status, string Cpf, DateTime DataNascimento)
        {
            this.Id = Id;
            this.NomeCliente = NomeCliente;
            this.Valor = Valor;
            this.Status = Status;
            this.DataNascimento = DataNascimento;
            this.Cpf = Cpf;
        }

        public Guid Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Status { get; set; } = "Em Análise"; 
        public string Cpf { get; set; } = string.Empty; 
        public DateTime DataNascimento { get; set; }
 

    }
}
