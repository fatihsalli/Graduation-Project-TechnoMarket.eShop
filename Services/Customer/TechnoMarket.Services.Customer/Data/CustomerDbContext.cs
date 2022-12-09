using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.Services.Customer.Models;

namespace TechnoMarket.Services.Customer.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add the Postgres Extension for UUID generation
            //modelBuilder.HasDefaultSchema("Fatih");

            //Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
