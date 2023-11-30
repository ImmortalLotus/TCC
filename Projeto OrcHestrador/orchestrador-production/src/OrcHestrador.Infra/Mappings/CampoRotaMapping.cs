using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrcHestrador.Domain.Models;

namespace OrcHestrador.Infra.Mappings
{
    public class CampoRotaMapping : IEntityTypeConfiguration<CampoRota>
    {
        public void Configure(EntityTypeBuilder<CampoRota> builder)
        {
            builder.ToTable("campos_rota");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CampoApi).HasColumnName("campo_api");
            builder.Property(x => x.IdRota).HasColumnName("Id_rota");

            builder.HasOne(x => x.Rota).WithMany(x => x.camposRota).HasForeignKey(x => x.IdRota);
        }
    }
}