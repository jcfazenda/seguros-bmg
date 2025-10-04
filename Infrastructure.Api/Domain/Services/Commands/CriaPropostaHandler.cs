using Infraestructure.Domain.Models;
using Infraestructure.Domain.Repository.Interface;
using Infraestructure.Domain.Views.Input;
using MediatR;

namespace Infraestructure.Domain.Services.Commands
{
public class CriaPropostaHandler : IRequestHandler<PropostaMadiatorInput, Propostas>
{
    private readonly IPropostaRepository _repository;

    public CriaPropostaHandler(IPropostaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Propostas> Handle(PropostaMadiatorInput request, CancellationToken cancellationToken)
    {
        var proposta = new Propostas
        {
            Id = Guid.NewGuid(),
            NomeCliente = request.NomeCliente,
            Cpf = request.Cpf,
            DataNascimento = request.DataNascimento,
            Valor = request.Valor,
            Status = request.Status
        };

        await _repository.AddAsync(proposta);
        return proposta;
    }
}

}
