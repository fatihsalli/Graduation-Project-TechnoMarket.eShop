using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Data.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = new Guid("46a02782-f572-4c86-860e-8f908fc105ce"),
                    Name = "Asus Zenbook",
                    Stock = 10,
                    Price = 40000,
                    Description = "12th gen Intel® Core™ i9 processor,32 GB memory,1 TB SSD storage",
                    ImageFile = "437dead8-dda3-4c46-85f6-9d0cd013629d.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("a53c1fd9-2b60-405f-a73b-847c641214a1"),
                },
                new Product
                {
                    Id = new Guid("7723714d-be34-438a-9f9e-57463d94dd5b"),
                    Name = "Iphone 14 Plus",
                    Stock = 50,
                    Price = 30000,
                    Description = "512 GB Capacity,6,7' display,A15 Bionic chip,Ceramic shield front, glass back and aluminium design",
                    ImageFile = "582a7fed-b175-4642-956d-58d1b06aeed6.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("c81bd97b-85ab-4cba-920a-73b5daab921f"),
                }
                );
        }
    }
}
