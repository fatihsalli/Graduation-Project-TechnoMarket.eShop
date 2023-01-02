using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Data.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = new Guid("539fad2d-dca1-4a26-a41f-987db7847583"), Name = "Notebook" },
                new Category { Id = new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"), Name = "Smart Phone" },
                new Category { Id = new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"), Name = "Home Equipment" }
                );
        }
    }
}
