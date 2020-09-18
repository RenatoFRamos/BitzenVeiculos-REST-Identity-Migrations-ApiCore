using Bitzen.Veiculos.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bitzen.Veiculos.Data.Mapping
{
    public class OportunidadeMapping : IEntityTypeConfiguration<Oportunidade>
    {
        public void Configure(EntityTypeBuilder<Oportunidade> builder)
        {

        }
    }
}
