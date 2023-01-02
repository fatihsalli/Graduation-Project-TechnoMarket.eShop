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
             new ProductFeature { Id = new Guid("c44ee253-20d5-4e19-9055-d661b9be5703"), Color = "Black", Height = "12'", Width = "15.3'", Weight = "2.5 kg" },
             new ProductFeature { Id = new Guid("1c32da1d-8b13-418b-bd78-5fe483bef59c"), Color = "White", Height = "14'", Width = "15.3'", Weight = "1.8 kg" },
             new ProductFeature { Id = new Guid("46ba7244-7d95-4274-bfba-ca374023d303"), Color = "Purple", Height = "6.33'", Width = "3.07'", Weight = "0.25 kg" },
             new ProductFeature { Id = new Guid("d6a4d2c5-9087-4278-a161-f96a142eb606"), Color = "Black", Height = "7.2'", Width = "3.6'", Weight = "0.40 kg" },
             new ProductFeature { Id = new Guid("a738ca5c-cf72-4faf-9f50-68037fb3f4d5"), Color = "White", Height = "2.2'", Width = "12.6'", Weight = "2.9 kg" },
             new ProductFeature { Id = new Guid("fcf628c8-44c0-40ed-b042-28367de2c0ad"), Color = "Silver", Height = "39.2'", Width = "6.6'", Weight = "4.9 kg" }
             );
        }
    }
}
