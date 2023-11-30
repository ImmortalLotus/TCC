using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrcHestrador.Domain.Models;

namespace OrcHestrador.Infra.Mappings
{
    public class MensagemMapping : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            builder.ToTable("mensagens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.UrlOrigem).HasColumnName("UrlOrigem");
            builder.Property(x => x.Message).HasColumnName("Mensagem");
            builder.Property(x => x.Status).HasColumnName("Status");
            builder.Property(x => x.TicketId).HasColumnName("TicketId");
        }
    }
}