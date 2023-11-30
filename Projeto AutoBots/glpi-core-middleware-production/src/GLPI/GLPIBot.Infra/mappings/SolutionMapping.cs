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
    internal class SolutionMapping : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.ToTable("glpi_itilsolutions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ItemType).HasColumnName("itemtype");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ItemsId).HasColumnName("items_id");
            builder.Property(x => x.SolutionTypesId).HasColumnName("solutiontypes_id");
            builder.Property(x=>x.Content).HasColumnName("content");
            builder.Property(x => x.UsersId).HasColumnName("users_id");
            builder.Property(x => x.DateMod).HasColumnName("date_mod");
            builder.Property(x => x.DateCreation).HasColumnName("date_creation");
            builder.Property(x=>x.Status).HasColumnName("status");
        }
    }
}
