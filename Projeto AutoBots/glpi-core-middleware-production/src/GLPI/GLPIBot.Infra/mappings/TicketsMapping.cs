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
    public class TicketsMapping : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("glpi_tickets");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Status).HasColumnName("status");

            builder.Property(x => x.Content).HasColumnName("content");
            builder.Property(x => x.ITILCategoriesId).HasColumnName("itilcategories_id");
            builder.Property(x => x.UsersIdRecipient).HasColumnName("users_id_recipient");
            builder.Property(x => x.SolveDate).HasColumnName("solvedate");
        }
    }
}
