
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructure.Context;
using Infraestructure.Domain.Models;
using Infraestructure.Domain.Repository.Interface;

namespace Infrastructure.Domain.Repository.Queryable
{
    public class PropostaRepository : IPropostaRepository
    {
        private readonly SeguroContext _context;

        public PropostaRepository(SeguroContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Propostas proposta)
        {
            await _context.AddAsync(proposta);
            await _context.SaveChangesAsync();
        }

        public async Task<Propostas> GetByIdAsync(Guid id)
        {
            var proposta = await _context.Proposta
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proposta == null) throw new InvalidOperationException($"Proposta {id} não encontrada");

            return proposta;
        }


        public async Task UpdateAsync(Propostas proposta)
        {
            _context.Update(proposta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Proposta.FindAsync(id); // <-- corrigido
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Propostas>> GetByPropostaAsync(string cpf)
        {
            return await _context.Proposta
                .AsNoTracking()
                .Where(p => p.NomeCliente.Contains(cpf))
                .ToListAsync();
        }

        public async Task<IEnumerable<Propostas>> GetByStatusAsync(string status)
        {
            return await _context.Proposta
                .AsNoTracking()
                .Where(p => p.Status == status)
                .ToListAsync();
        }

        // Atualizações pontuais
        public async Task<bool> UpdateStatusAsync(Guid id, string status)
        {
            var proposta = await _context.Proposta.FindAsync(id); // <-- corrigido
            if (proposta == null) return false;

            proposta.Status = status;  // <-- aqui faltava usar 'proposta'
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateValorAsync(Guid id, decimal valor)
        {
            var proposta = await _context.Proposta.FindAsync(id); // <-- corrigido
            if (proposta == null) return false;

            proposta.Valor = valor;  // <-- aqui também
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
