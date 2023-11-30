using Microsoft.EntityFrameworkCore;
using OrcHestrador.Domain.Models;
using OrcHestrador.Infra.Mappings;

namespace OrcHestrador.Infra.Context
{
    public class OrcHestradorContext : DbContext
    {
        public OrcHestradorContext(DbContextOptions<OrcHestradorContext> options) : base(options)
        { }

        public DbSet<Tickets> tickets { get; set; }
        public DbSet<TicketRequest> ticketsRequest { get; set; }
        public DbSet<TicketComErro> ticketsComErro { get; set; }
        public DbSet<Automacao> automacoes { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Rota> rotas { get; set; }
        public DbSet<CampoRota> camposRota { get; set; }
        public DbSet<Mensagem> mensagens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder.ApplyConfiguration(new TIcketsComErroMapping());
            modelBuilder.ApplyConfiguration(new TicketRequestMapping());
            modelBuilder.ApplyConfiguration(new TicketsMapping());
            modelBuilder.ApplyConfiguration(new AutomacaoMapping());
            modelBuilder.ApplyConfiguration(new CategoriaMapping());
            modelBuilder.ApplyConfiguration(new MensagemMapping());
            modelBuilder.ApplyConfiguration(new RotaMapping());
            modelBuilder.ApplyConfiguration(new CampoRotaMapping());
        }
    }
}