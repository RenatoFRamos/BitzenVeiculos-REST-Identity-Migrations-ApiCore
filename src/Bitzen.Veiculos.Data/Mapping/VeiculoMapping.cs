using Bitzen.Veiculos.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bitzen.Veiculos.Data.Mapping
{
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Modelo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Placa)
                .IsRequired()
                .HasColumnType("varchar(7)");

            builder.Property(p => p.Ano)
                .IsRequired()
                .HasColumnType("varchar(4)");

            builder.Property(p => p.Combustivel)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Veiculos");
        }
    }
}
