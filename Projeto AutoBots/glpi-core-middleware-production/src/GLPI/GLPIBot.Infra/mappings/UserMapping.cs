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
    //O name é um campo onde é atualmente armazenado o CPF do indivíduo no GLPI. 
    public class UserMapping : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("glpi_users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");
        }
    }
}
