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
    internal class ITILFollowupMapping : IEntityTypeConfiguration<ITILFollowUp>
    {
        public void Configure(EntityTypeBuilder<ITILFollowUp> builder)
        {
            builder.ToTable("glpi_itilfollowups");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ItemType).HasColumnName("itemtype");
            builder.Property(x => x.ItemsId).HasColumnName("items_id");
            builder.Property(x => x.UsersId).HasColumnName("users_id");
            builder.Property(x => x.UsersIdEditor).HasColumnName("users_id_editor");
            builder.Property(x => x.IsPrivate).HasColumnName("is_private");
            builder.Property(x => x.RequestTypesId).HasColumnName("requesttypes_id");
            builder.Property(x => x.TimeLinePosition).HasColumnName("timeline_position");
            builder.Property(x => x.SourceItemsId).HasColumnName("sourceitems_id");
            builder.Property(x => x.SourceOfItemsId).HasColumnName("sourceof_items_id");
            builder.Property(x=>x.Content).HasColumnName("content");
            builder.Property(x=>x.Date).HasColumnName("date");
            builder.Property(x => x.DateMod).HasColumnName("date_mod");
            builder.Property(x => x.DateCreation).HasColumnName("date_creation");
        }
    }
}
