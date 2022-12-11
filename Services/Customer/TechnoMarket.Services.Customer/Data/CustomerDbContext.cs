using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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



        //SaveChange metodunu eziyoruz.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is Models.Customer entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            break;
                        case EntityState.Modified:
                            Entry(entityReference).Property(x => x.CreatedAt).IsModified = false;
                            entityReference.UpdatedAt = DateTime.Now;
                            break;
                        case EntityState.Added:
                            entityReference.CreatedAt = DateTime.Now;
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Add the Postgres Extension for UUID generation
            //modelBuilder.HasDefaultSchema("Fatih");

            //Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CustomerDbContext)));

            base.OnModelCreating(modelBuilder);
        }

    }
}
