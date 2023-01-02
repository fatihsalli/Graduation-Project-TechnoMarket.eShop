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
                new Category { Id = new Guid("a53c1fd9-2b60-405f-a73b-847c641214a1"), Name = "Notebook" },
                new Category { Id = new Guid("c81bd97b-85ab-4cba-920a-73b5daab921f"), Name = "Smart Phone" },
                new Category { Id = new Guid("a385dfb1-609c-4c78-bc00-fd0a3ef75b00"), Name = "Home Equipment" }
                );
        }
    }
}
