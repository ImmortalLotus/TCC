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
    internal class TicketUsersMapping : IEntityTypeConfiguration<TicketUsers>
    {
        public void Configure(EntityTypeBuilder<TicketUsers> builder)
        {
            builder.ToTable("glpi_tickets_users");
            builder.HasKey(x=>x.Id);

            builder.Property(x => x.UsersId).HasColumnName("users_id");
            builder.Property(x => x.TicketId).HasColumnName("tickets_id");
            builder.Property(x => x.Type).HasColumnName("type");

        }
    }
}
