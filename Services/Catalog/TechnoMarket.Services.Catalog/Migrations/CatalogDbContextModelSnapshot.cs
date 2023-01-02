﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnoMarket.Services.Catalog.Data;

#nullable disable

namespace TechnoMarket.Services.Catalog.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TechnoMarket.Services.Catalog.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("539fad2d-dca1-4a26-a41f-987db7847583"),
                            Name = "Notebook"
                        },
                        new
                        {
                            Id = new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"),
                            Name = "Smart Phone"
                        },
                        new
                        {
                            Id = new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"),
                            Name = "Home Equipment"
                        });
                });

            modelBuilder.Entity("TechnoMarket.Services.Catalog.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("CategoryId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Picture")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c44ee253-20d5-4e19-9055-d661b9be5703"),
                            CategoryId = new Guid("539fad2d-dca1-4a26-a41f-987db7847583"),
                            CreatedAt = new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8106),
                            Description = "ASUS ZenBook 14 Ultra-Slim Laptop 14” FHD Display, AMD Ryzen 7 5800H CPU, Radeon Vega 7 Graphics, 16GB RAM, 1TB PCIe SSD, NumberPad, Windows 11 Pro, Pine Grey, UM425QA-EH74",
                            Name = "Asus Zenbook",
                            Picture = "437dead8-dda3-4c46-85f6-9d0cd013629d.jpg",
                            Price = 1200m,
                            Stock = 10
                        },
                        new
                        {
                            Id = new Guid("1c32da1d-8b13-418b-bd78-5fe483bef59c"),
                            CategoryId = new Guid("539fad2d-dca1-4a26-a41f-987db7847583"),
                            CreatedAt = new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8118),
                            Description = "Apple 2022 MacBook Pro Laptop with M2 chip: 13-inch Retina Display, 8GB RAM, 256GB ​​​​​​​SSD ​​​​​​​Storage, Touch Bar, Backlit Keyboard, FaceTime HD Camera. Works with iPhone and iPad; Space Gray",
                            Name = "Macbook Pro",
                            Picture = "d78aba71-fab4-499b-8dc6-ebf797a5c38e.jpg",
                            Price = 1300m,
                            Stock = 10
                        },
                        new
                        {
                            Id = new Guid("46ba7244-7d95-4274-bfba-ca374023d303"),
                            CategoryId = new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"),
                            CreatedAt = new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8121),
                            Description = "512 GB Capacity,6,7' display,A15 Bionic chip,Ceramic shield front, glass back and aluminium design",
                            Name = "Iphone 14 Plus",
                            Picture = "b84407d0-c0f5-4ae5-9746-1bec572953b6.jpg",
                            Price = 950m,
                            Stock = 50
                        },
                        new
                        {
                            Id = new Guid("d6a4d2c5-9087-4278-a161-f96a142eb606"),
                            CategoryId = new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"),
                            CreatedAt = new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8123),
                            Description = "SAMSUNG Galaxy S22 Cell Phone, Factory Unlocked Android Smartphone, 128GB, 8K Camera & Video, Night Mode, Brightest Display Screen, 50MP Photo Resolution, Long Battery Life, US Version, Green",
                            Name = "Samsung Galaxy S22",
                            Picture = "9849e8af-83bb-409b-9ef4-9a4535b26f09.jpg",
                            Price = 750m,
                            Stock = 12
                        },
                        new
                        {
                            Id = new Guid("a738ca5c-cf72-4faf-9f50-68037fb3f4d5"),
                            CategoryId = new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"),
                            CreatedAt = new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8125),
                            Description = "Roborock S5 MAX Robot Vacuum and Mop, Self-Charging Robotic Vacuum Cleaner, Lidar Navigation, Selective Room Cleaning, No-mop Zones, 2000Pa Powerful Suction, 180min Runtime, Works with Alexa(White)",
                            Name = "Roborock S5 Max",
                            Picture = "97c97f97-d398-4962-8afa-d2c960d78e14.jpg",
                            Price = 650m,
                            Stock = 10
                        },
                        new
                        {
                            Id = new Guid("fcf628c8-44c0-40ed-b042-28367de2c0ad"),
                            CategoryId = new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"),
                            CreatedAt = new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8129),
                            Description = "Dyson V15 Detect Absolute Cordless Vacuum Cleaner 60 Mins Run 2 Year",
                            Name = "Dyson V15 Detect",
                            Picture = "b44fd94f-12ff-4b4d-acdb-e0db37219bae.jpg",
                            Price = 600m,
                            Stock = 10
                        });
                });

            modelBuilder.Entity("TechnoMarket.Services.Catalog.Models.ProductFeature", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Height")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Width")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ProductFeatures");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c44ee253-20d5-4e19-9055-d661b9be5703"),
                            Color = "Black",
                            Height = "12'",
                            Weight = "2.5 kg",
                            Width = "15.3'"
                        },
                        new
                        {
                            Id = new Guid("1c32da1d-8b13-418b-bd78-5fe483bef59c"),
                            Color = "White",
                            Height = "14'",
                            Weight = "1.8 kg",
                            Width = "15.3'"
                        },
                        new
                        {
                            Id = new Guid("46ba7244-7d95-4274-bfba-ca374023d303"),
                            Color = "Purple",
                            Height = "6.33'",
                            Weight = "0.25 kg",
                            Width = "3.07'"
                        },
                        new
                        {
                            Id = new Guid("d6a4d2c5-9087-4278-a161-f96a142eb606"),
                            Color = "Black",
                            Height = "7.2'",
                            Weight = "0.40 kg",
                            Width = "3.6'"
                        },
                        new
                        {
                            Id = new Guid("a738ca5c-cf72-4faf-9f50-68037fb3f4d5"),
                            Color = "White",
                            Height = "2.2'",
                            Weight = "2.9 kg",
                            Width = "12.6'"
                        },
                        new
                        {
                            Id = new Guid("fcf628c8-44c0-40ed-b042-28367de2c0ad"),
                            Color = "Silver",
                            Height = "39.2'",
                            Weight = "4.9 kg",
                            Width = "6.6'"
                        });
                });

            modelBuilder.Entity("TechnoMarket.Services.Catalog.Models.Product", b =>
                {
                    b.HasOne("TechnoMarket.Services.Catalog.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("TechnoMarket.Services.Catalog.Models.ProductFeature", b =>
                {
                    b.HasOne("TechnoMarket.Services.Catalog.Models.Product", "Product")
                        .WithOne("Feature")
                        .HasForeignKey("TechnoMarket.Services.Catalog.Models.ProductFeature", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TechnoMarket.Services.Catalog.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TechnoMarket.Services.Catalog.Models.Product", b =>
                {
                    b.Navigation("Feature");
                });
#pragma warning restore 612, 618
        }
    }
}
