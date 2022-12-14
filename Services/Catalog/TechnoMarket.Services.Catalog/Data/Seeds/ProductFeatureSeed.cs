using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Data.Seeds
{
    public class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(
             new ProductFeature { Id = new Guid("46a02782-f572-4c86-860e-8f908fc105ce"), Color = "Black", Height = "12'", Width = "15.3'", Weight = "2.5 kg" },
             new ProductFeature { Id = new Guid("7723714d-be34-438a-9f9e-57463d94dd5b"), Color = "Purple", Height = "6.33'", Width = "3.07'", Weight = "0.25 kg" }
             );
        }
    }
}
