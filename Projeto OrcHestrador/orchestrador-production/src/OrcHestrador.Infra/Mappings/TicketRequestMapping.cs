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
    public class TicketRequestMapping : IEntityTypeConfiguration<TicketRequest>
    {
        public void Configure(EntityTypeBuilder<TicketRequest> builder)
        {
            builder.ToTable("TicketRequest");
            builder.HasKey(x => x.TicketId);
            builder.Property(x => x.TicketId).HasColumnName("TicketId");
            builder.Property(x => x.IdCategoria).HasColumnName("IdCategoria");
            builder.Property(x => x.Json).HasColumnName("Json");
            builder.Property(x => x.Status).HasColumnName("Status");
        }
    }
}
