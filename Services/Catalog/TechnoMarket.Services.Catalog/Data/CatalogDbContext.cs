using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is ProductFeature entityReference2)
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
                            var test1 = 200;
                            var test2 = 200;

                            break;
                        case EntityState.Added:
                            var test3 = 200;
                            var test4 = 200;

                            break;
                    }
                }






                if (item.Entity is Product entityReference)
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
                            var test1 = 200;
                            var test2 = 200;

                            break;
                        case EntityState.Added:  
                            var test3 = 200;
                            var test4 = 200;

                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CatalogDbContext)));

            base.OnModelCreating(modelBuilder);
        }



    }
}
