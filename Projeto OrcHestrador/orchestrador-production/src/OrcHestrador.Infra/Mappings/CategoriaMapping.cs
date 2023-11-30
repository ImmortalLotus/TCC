using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrcHestrador.Domain.Models;

namespace OrcHestrador.Infra.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("categorias");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Descricao).HasColumnName("Descricao");
            builder.Property(x => x.tipoValidacao).HasColumnName("TipoValidacao").HasConversion<string>();
        }
    }
}