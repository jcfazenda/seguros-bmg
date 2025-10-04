using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infraestructure.Domain.Models;

namespace Infraestructure.Domain.Mapping
{
    public class PropostaMap : IEntityTypeConfiguration<Propostas>
    {
        public void Configure(EntityTypeBuilder<Propostas> builder)
        {
            builder.ToTable("Proposta");

            builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
            builder.HasKey(o => o.Id);

            builder.Property(p => p.NomeCliente)
                   .HasColumnName("NomeCliente")
                   .IsRequired();

            builder.Property(p => p.Valor)
                   .HasColumnName("Valor")
                   .IsRequired();

            builder.Property(p => p.Status)
                   .HasColumnName("Status")
                   .IsRequired();

            builder.Property(p => p.DataNascimento)
                   .HasColumnName("DataNascimento")
                   .IsRequired();

            builder.Property(p => p.Cpf)
                   .HasColumnName("Cpf")
                   .IsRequired();
        }
    }
}
