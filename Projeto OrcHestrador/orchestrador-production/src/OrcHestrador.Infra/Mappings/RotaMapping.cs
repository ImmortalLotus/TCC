using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrcHestrador.Domain.Models;

namespace OrcHestrador.Infra.Mappings
{
    public class RotaMapping : IEntityTypeConfiguration<Rota>
    {
        public void Configure(EntityTypeBuilder<Rota> builder)
        {
            builder.ToTable("rotas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.RotaApi).HasColumnName("rota_api");
            builder.Property(x => x.IdAutomacao).HasColumnName("Id_automacao");
            builder.Property(x => x.IdCategoria).HasColumnName("Id_categoria");

            builder.HasOne(x => x.Categoria).WithMany(x => x.rotas).HasForeignKey(x => x.IdCategoria);
            builder.HasOne(x => x.Automacao).WithMany(x => x.rotas).HasForeignKey(x => x.IdAutomacao);
        }
    }
}