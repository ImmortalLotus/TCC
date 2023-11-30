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
    internal class GroupsMapping : IEntityTypeConfiguration<Groups>
    {
        public void Configure(EntityTypeBuilder<Groups> builder)
        {
            builder.ToTable("glpi_groups");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.CompleteName).HasColumnName("completename");
        }
    }
}
