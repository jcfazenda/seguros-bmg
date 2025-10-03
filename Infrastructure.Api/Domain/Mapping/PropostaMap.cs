using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Infraestructure.Domain.Models;

namespace Infraestructure.Domain.Mapping
{
    public class PropostaMap : IEntityTypeConfiguration<Propostas>
    {
        public void Configure(EntityTypeBuilder<Propostas> constuctor)
        {
            constuctor.ToTable("Propostas");
            constuctor.Property(m => m.Id).HasColumnName("Id").IsRequired();
            constuctor.HasKey(o => o.Id);

            constuctor.Property(p => p.NomeCliente).IsRequired();
            constuctor.Property(p => p.Valor).IsRequired();
            constuctor.Property(p => p.Status).IsRequired();
        }
    }
}
