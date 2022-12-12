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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CatalogDbContext)));

            base.OnModelCreating(modelBuilder);
        }



    }
}
