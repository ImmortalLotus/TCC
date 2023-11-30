using GLPIBot.GLPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.Infra.mappings
{
    internal class GroupTicketsMapping : IEntityTypeConfiguration<Group_Ticket>
    {
        public void Configure(EntityTypeBuilder<Group_Ticket> builder)
        {
            builder.ToTable("glpi_groups_tickets");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Tickets_id).HasColumnName("tickets_id");
            builder.Property(x => x.Groups_id).HasColumnName("groups_id");
            builder.Property(x => x.Type).HasColumnName("type");
        }
    }
}
