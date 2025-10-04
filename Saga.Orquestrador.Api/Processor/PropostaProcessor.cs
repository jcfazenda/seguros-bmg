
using Saga.Orquestrador.Api.Models;
using Saga.Orquestrador.Api.Views.Input;

namespace Saga.Orquestrador.Api.Processor
{
    public static class PropostaProcessor
    {
        public static Propostas Processar(PropostaInput input)
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("SAGA ouviu a Rabbit");

            // Calcula idade
            int idade = DateTime.Today.Year - input.DataNascimento.Year;
            if (input.DataNascimento.Date > DateTime.Today.AddYears(-idade))
                idade--;

            // Define status
            string status = idade switch
            {
                < 18 => "Rejeitada",
                >= 18 and <= 49 => "Em Análise",
                >= 50 => "Aprovada"
            };

            Console.WriteLine("Proposta Recebida:");
            Console.WriteLine();
            Console.WriteLine($"Nome Cliente : {input.NomeCliente}");
            Console.WriteLine($"Status       : {input.Status}");
            Console.WriteLine($"CPF          : {input.Cpf}");
            Console.WriteLine($"Data Nasc.   : {input.DataNascimento:dd/MM/yyyy}");
            Console.WriteLine($"Valor        : {input.Valor:C}");
            Console.WriteLine();

            return new Propostas
            {
                Id = Guid.NewGuid(),
                NomeCliente = input.NomeCliente,
                Cpf = input.Cpf,
                DataNascimento = input.DataNascimento,
                Valor = input.Valor,
                Status = status
            };
        }
    }

}