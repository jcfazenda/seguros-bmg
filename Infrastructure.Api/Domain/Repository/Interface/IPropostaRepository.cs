using Infraestructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructure.Domain.Views.Input;

namespace Infraestructure.Domain.Repository.Interface
{
    public interface IPropostaRepository
    {
        Task<Propostas> CreateAsync(PropostaInput input);
        Task AddAsync(Propostas Propostas);
        Task<Propostas> GetByIdAsync(Guid id);
        Task UpdateAsync(Propostas Proposta);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<Propostas>> GetByPropostaAsync(string cpf);
        Task<IEnumerable<Propostas>> GetByStatusAsync(string status);

        Task<bool> UpdateStatusAsync(Guid id, string status);
        Task<bool> UpdateValorAsync(Guid id, decimal valor); 
    }
}
