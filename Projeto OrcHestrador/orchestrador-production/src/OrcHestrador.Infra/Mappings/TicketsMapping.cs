using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrcHestrador.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.Infra.Mappings
{
    public class TicketsMapping : IEntityTypeConfiguration<Tickets>
    {
        public void Configure(EntityTypeBuilder<Tickets> builder)
        {
            builder.ToTable("tickets");
            builder.HasKey(x => x.TicketId);
            builder.Property(x => x.TicketId).HasColumnName("ticketId");

            builder.Property(x => x.SituacaoChamado).HasColumnName("situacaoChamado").HasConversion<string>();

            builder.Property(x => x.Categoria).HasColumnName("categoria");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.IdUsuario).HasColumnName("idUsuario");
        }
    }
}
