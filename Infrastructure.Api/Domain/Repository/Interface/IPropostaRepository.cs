using Infraestructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestructure.Domain.Repository.Interface
{
    public interface IPropostaRepository
    {
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
