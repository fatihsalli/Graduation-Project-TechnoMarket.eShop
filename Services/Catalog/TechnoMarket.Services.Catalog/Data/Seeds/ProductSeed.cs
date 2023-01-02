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
                    Id = new Guid("c44ee253-20d5-4e19-9055-d661b9be5703"),
                    Name = "Asus Zenbook",
                    Stock = 10,
                    Price = 1200,
                    Description = "ASUS ZenBook 14 Ultra-Slim Laptop 14” FHD Display, AMD Ryzen 7 5800H CPU, Radeon Vega 7 Graphics, 16GB RAM, 1TB PCIe SSD, NumberPad, Windows 11 Pro, Pine Grey, UM425QA-EH74",
                    Picture = "437dead8-dda3-4c46-85f6-9d0cd013629d.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("539fad2d-dca1-4a26-a41f-987db7847583"),
                },
                new Product
                {
                    Id = new Guid("1c32da1d-8b13-418b-bd78-5fe483bef59c"),
                    Name = "Macbook Pro",
                    Stock = 10,
                    Price = 1300,
                    Description = "Apple 2022 MacBook Pro Laptop with M2 chip: 13-inch Retina Display, 8GB RAM, 256GB ​​​​​​​SSD ​​​​​​​Storage, Touch Bar, Backlit Keyboard, FaceTime HD Camera. Works with iPhone and iPad; Space Gray",
                    Picture = "d78aba71-fab4-499b-8dc6-ebf797a5c38e.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("539fad2d-dca1-4a26-a41f-987db7847583"),
                },
                new Product
                {
                    Id = new Guid("46ba7244-7d95-4274-bfba-ca374023d303"),
                    Name = "Iphone 14 Plus",
                    Stock = 50,
                    Price = 950,
                    Description = "512 GB Capacity,6,7' display,A15 Bionic chip,Ceramic shield front, glass back and aluminium design",
                    Picture = "b84407d0-c0f5-4ae5-9746-1bec572953b6.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"),
                },
                new Product
                {
                    Id = new Guid("d6a4d2c5-9087-4278-a161-f96a142eb606"),
                    Name = "Samsung Galaxy S22",
                    Stock = 12,
                    Price = 750,
                    Description = "SAMSUNG Galaxy S22 Cell Phone, Factory Unlocked Android Smartphone, 128GB, 8K Camera & Video, Night Mode, Brightest Display Screen, 50MP Photo Resolution, Long Battery Life, US Version, Green",
                    Picture = "9849e8af-83bb-409b-9ef4-9a4535b26f09.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"),
                },
                new Product
                {
                    Id = new Guid("a738ca5c-cf72-4faf-9f50-68037fb3f4d5"),
                    Name = "Roborock S5 Max",
                    Stock = 10,
                    Price = 650,
                    Description = "Roborock S5 MAX Robot Vacuum and Mop, Self-Charging Robotic Vacuum Cleaner, Lidar Navigation, Selective Room Cleaning, No-mop Zones, 2000Pa Powerful Suction, 180min Runtime, Works with Alexa(White)",
                    Picture = "97c97f97-d398-4962-8afa-d2c960d78e14.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"),
                },
                new Product
                {
                    Id = new Guid("fcf628c8-44c0-40ed-b042-28367de2c0ad"),
                    Name = "Dyson V15 Detect",
                    Stock = 10,
                    Price = 600,
                    Description = "Dyson V15 Detect Absolute Cordless Vacuum Cleaner 60 Mins Run 2 Year",
                    Picture = "b44fd94f-12ff-4b4d-acdb-e0db37219bae.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"),
                }
                );

        }
    }
}
