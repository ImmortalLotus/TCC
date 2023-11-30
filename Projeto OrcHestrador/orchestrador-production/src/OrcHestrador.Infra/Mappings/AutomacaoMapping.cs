using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrcHestrador.Domain.Models;

namespace OrcHestrador.Infra.Mappings
{
    public class AutomacaoMapping : IEntityTypeConfiguration<Automacao>
    {
        public void Configure(EntityTypeBuilder<Automacao> builder)
        {
            builder.ToTable("automacoes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Url).HasColumnName("api_url");
            builder.Property(x => x.NomeAutomacao).HasColumnName("nome_automacao");
            builder.Property(x => x.SenhaAutomacao).HasColumnName("senha_automacao");
        }
    }
}