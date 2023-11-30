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
    internal class TicketValidationsMapping : IEntityTypeConfiguration<TicketValidations>
    {
        public void Configure(EntityTypeBuilder<TicketValidations> builder)
        {
            builder.ToTable("glpi_ticketvalidations");
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.EntitiesId).HasColumnName("entities_id");
            builder.Property(x => x.UsersId).HasColumnName("users_id");
            builder.Property(x => x.UsersIdValidate).HasColumnName("users_id_validate");
            builder.Property(x => x.TicketsId).HasColumnName("tickets_id");
        }
    }
}
