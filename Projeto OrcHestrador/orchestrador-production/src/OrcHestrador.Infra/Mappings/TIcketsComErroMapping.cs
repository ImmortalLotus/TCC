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
    public class TIcketsComErroMapping : IEntityTypeConfiguration<TicketComErro>
    {
        public void Configure(EntityTypeBuilder<TicketComErro> builder)
        {
            builder.ToTable("TicketComErro");
            builder.HasKey(x => x.TicketId);
            builder.Property(x => x.TicketId).HasColumnName("TicketId");
            builder.Property(x => x.Erro).HasColumnName("Erro");
        }
    }

    
}
