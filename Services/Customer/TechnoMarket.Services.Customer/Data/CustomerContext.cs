using Microsoft.EntityFrameworkCore;

namespace TechnoMarket.Services.Customer.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);



            base.OnModelCreating(modelBuilder);
        }

    }
}
