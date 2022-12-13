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
                if (item.Entity is ProductFeature productFeature)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            Entry(productFeature).Property(x => x.Id).IsModified = false;
                            Entry(productFeature).Property(x => x.Product).IsModified = false;

                            break;
                        case EntityState.Added:
                            Entry(productFeature).Property(x => x.Id).IsModified = false;
                            Entry(productFeature).Property(x => x.Product).IsModified = false;

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

                            break;
                        case EntityState.Added:
       
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
