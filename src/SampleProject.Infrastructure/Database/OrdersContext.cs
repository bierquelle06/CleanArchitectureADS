using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Plannings;
using SampleProject.Infrastructure.Processing.InternalCommands;
using SampleProject.Infrastructure.Processing.Outbox;

namespace SampleProject.Infrastructure.Database
{
    public class OrdersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Planning> Plannings { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public OrdersContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
        }
    }
}
