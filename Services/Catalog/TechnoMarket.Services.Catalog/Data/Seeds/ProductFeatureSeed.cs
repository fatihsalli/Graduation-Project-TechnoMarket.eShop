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
             new ProductFeature { Id = new Guid("42f02782-f572-4c86-860e-8f908fc106cv"), Color = "White", Height = "14'", Width = "15.3'", Weight = "1.8 kg" },
             new ProductFeature { Id = new Guid("7723714d-be34-438a-9f9e-57463d94dd5b"), Color = "Purple", Height = "6.33'", Width = "3.07'", Weight = "0.25 kg" },
             new ProductFeature { Id = new Guid("6653714d-be34-438a-9f9e-57463d94dd5c"), Color = "Black", Height = "7.2'", Width = "3.6'", Weight = "0.40 kg" },
             new ProductFeature { Id = new Guid("dfb2a92e-4f60-4eea-b82f-8b405d28f37b"), Color = "White", Height = "2.2'", Width = "12.6'", Weight = "2.9 kg" },
             new ProductFeature { Id = new Guid("e80b4733-02d8-4ce4-9b84-03a169bb4ec6"), Color = "Silver", Height = "39.2'", Width = "6.6'", Weight = "4.9 kg" }
             );
        }
    }
}
