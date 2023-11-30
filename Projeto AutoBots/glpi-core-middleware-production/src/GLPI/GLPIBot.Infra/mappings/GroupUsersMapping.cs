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
    public class GroupUsersMapping : IEntityTypeConfiguration<GroupUsers>
    {
        public void Configure(EntityTypeBuilder<GroupUsers> builder)
        {
            builder.ToTable("glpi_groups_users");
            //tem chave mas para o caso atual, não é necessário considerá-la.
            builder.HasNoKey();
            builder.Property(x => x.UserId).HasColumnName("users_id");
            builder.Property(x => x.GroupId).HasColumnName("groups_id");
        }
    }
}
