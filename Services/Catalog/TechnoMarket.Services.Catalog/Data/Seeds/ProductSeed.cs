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
                    Price = 1200,
                    Description = "ASUS ZenBook 14 Ultra-Slim Laptop 14” FHD Display, AMD Ryzen 7 5800H CPU, Radeon Vega 7 Graphics, 16GB RAM, 1TB PCIe SSD, NumberPad, Windows 11 Pro, Pine Grey, UM425QA-EH74",
                    Picture = "437dead8-dda3-4c46-85f6-9d0cd013629d.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("a53c1fd9-2b60-405f-a73b-847c641214a1"),
                },
                new Product
                {
                    Id = new Guid("42f02782-f572-4c86-860e-8f908fc106cv"),
                    Name = "Macbook Pro",
                    Stock = 10,
                    Price = 1300,
                    Description = "Apple 2022 MacBook Pro Laptop with M2 chip: 13-inch Retina Display, 8GB RAM, 256GB ​​​​​​​SSD ​​​​​​​Storage, Touch Bar, Backlit Keyboard, FaceTime HD Camera. Works with iPhone and iPad; Space Gray",
                    Picture = "d78aba71-fab4-499b-8dc6-ebf797a5c38e.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("a53c1fd9-2b60-405f-a73b-847c641214a1"),
                },
                new Product
                {
                    Id = new Guid("7723714d-be34-438a-9f9e-57463d94dd5b"),
                    Name = "Iphone 14 Plus",
                    Stock = 50,
                    Price = 950,
                    Description = "512 GB Capacity,6,7' display,A15 Bionic chip,Ceramic shield front, glass back and aluminium design",
                    Picture = "b84407d0-c0f5-4ae5-9746-1bec572953b6.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("c81bd97b-85ab-4cba-920a-73b5daab921f"),
                },
                new Product
                {
                    Id = new Guid("6653714d-be34-438a-9f9e-57463d94dd5c"),
                    Name = "Samsung Galaxy S22",
                    Stock = 12,
                    Price = 750,
                    Description = "SAMSUNG Galaxy S22 Cell Phone, Factory Unlocked Android Smartphone, 128GB, 8K Camera & Video, Night Mode, Brightest Display Screen, 50MP Photo Resolution, Long Battery Life, US Version, Green",
                    Picture = "9849e8af-83bb-409b-9ef4-9a4535b26f09.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("c81bd97b-85ab-4cba-920a-73b5daab921f"),
                },
                new Product
                {
                    Id = new Guid("dfb2a92e-4f60-4eea-b82f-8b405d28f37b"),
                    Name = "Roborock S5 Max",
                    Stock = 10,
                    Price = 650,
                    Description = "Roborock S5 MAX Robot Vacuum and Mop, Self-Charging Robotic Vacuum Cleaner, Lidar Navigation, Selective Room Cleaning, No-mop Zones, 2000Pa Powerful Suction, 180min Runtime, Works with Alexa(White)",
                    Picture = "97c97f97-d398-4962-8afa-d2c960d78e14.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("a385dfb1-609c-4c78-bc00-fd0a3ef75b00"),
                },
                new Product
                {
                    Id = new Guid("e80b4733-02d8-4ce4-9b84-03a169bb4ec6"),
                    Name = "Dyson V15 Detect",
                    Stock = 10,
                    Price = 600,
                    Description = "Dyson V15 Detect Absolute Cordless Vacuum Cleaner 60 Mins Run 2 Year",
                    Picture = "b44fd94f-12ff-4b4d-acdb-e0db37219bae.jpg",
                    CreatedAt = DateTime.Now,
                    CategoryId = new Guid("a385dfb1-609c-4c78-bc00-fd0a3ef75b00"),
                }
                );

        }
    }
}
