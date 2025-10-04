using Infraestructure.Domain.Mapping;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Domain.Models;

namespace Infraestructure.Context
{
    public class SeguroContext : DbContext
    {
        public SeguroContext(DbContextOptions<SeguroContext> options)
            : base(options)
        {
        }

        public DbSet<Propostas> Proposta { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PropostaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
