using GLPIBot.GLPI.Domain.Models;
using GLPIBot.Infra.mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.Infra
{
    public class GlpiContext : DbContext
    {
        public GlpiContext(DbContextOptions<GlpiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder.ApplyConfiguration(new GroupTicketsMapping());
            modelBuilder.ApplyConfiguration(new TicketsMapping());
            modelBuilder.ApplyConfiguration(new ITILFollowupMapping());
            modelBuilder.ApplyConfiguration(new TicketUsersMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new SolutionMapping());
            modelBuilder.ApplyConfiguration(new TicketValidationsMapping());
        }

        public DbSet<Group_Ticket> GroupsTickets => Set<Group_Ticket>();
        public DbSet<Solution> Solutions => Set<Solution>();
        public DbSet<TicketValidations> TicketValidations => Set<TicketValidations>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<TicketUsers> TicketUsers => Set<TicketUsers>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<ITILFollowUp> FollowUps => Set<ITILFollowUp>();
    }
}
