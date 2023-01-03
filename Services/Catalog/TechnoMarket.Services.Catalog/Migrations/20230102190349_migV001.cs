using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechnoMarket.Services.Catalog.Migrations
{
    /// <inheritdoc />
    public partial class migV001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Height = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Width = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeatures_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("539fad2d-dca1-4a26-a41f-987db7847583"), "Notebook" },
                    { new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"), "Smart Phone" },
                    { new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"), "Home Equipment" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Picture", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1c32da1d-8b13-418b-bd78-5fe483bef59c"), new Guid("539fad2d-dca1-4a26-a41f-987db7847583"), new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8118), "Apple 2022 MacBook Pro Laptop with M2 chip: 13-inch Retina Display, 8GB RAM, 256GB ​​​​​​​SSD ​​​​​​​Storage, Touch Bar, Backlit Keyboard, FaceTime HD Camera. Works with iPhone and iPad; Space Gray", "Macbook Pro", "d78aba71-fab4-499b-8dc6-ebf797a5c38e.jpg", 1300m, 10, null },
                    { new Guid("46ba7244-7d95-4274-bfba-ca374023d303"), new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"), new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8121), "512 GB Capacity,6,7' display,A15 Bionic chip,Ceramic shield front, glass back and aluminium design", "Iphone 14 Plus", "b84407d0-c0f5-4ae5-9746-1bec572953b6.jpg", 950m, 50, null },
                    { new Guid("a738ca5c-cf72-4faf-9f50-68037fb3f4d5"), new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"), new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8125), "Roborock S5 MAX Robot Vacuum and Mop, Self-Charging Robotic Vacuum Cleaner, Lidar Navigation, Selective Room Cleaning, No-mop Zones, 2000Pa Powerful Suction, 180min Runtime, Works with Alexa(White)", "Roborock S5 Max", "97c97f97-d398-4962-8afa-d2c960d78e14.jpg", 650m, 10, null },
                    { new Guid("c44ee253-20d5-4e19-9055-d661b9be5703"), new Guid("539fad2d-dca1-4a26-a41f-987db7847583"), new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8106), "ASUS ZenBook 14 Ultra-Slim Laptop 14” FHD Display, AMD Ryzen 7 5800H CPU, Radeon Vega 7 Graphics, 16GB RAM, 1TB PCIe SSD, NumberPad, Windows 11 Pro, Pine Grey, UM425QA-EH74", "Asus Zenbook", "437dead8-dda3-4c46-85f6-9d0cd013629d.jpg", 1200m, 10, null },
                    { new Guid("d6a4d2c5-9087-4278-a161-f96a142eb606"), new Guid("ccc4a6ab-8ba5-4198-92c4-bd89af052f05"), new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8123), "SAMSUNG Galaxy S22 Cell Phone, Factory Unlocked Android Smartphone, 128GB, 8K Camera & Video, Night Mode, Brightest Display Screen, 50MP Photo Resolution, Long Battery Life, US Version, Green", "Samsung Galaxy S22", "9849e8af-83bb-409b-9ef4-9a4535b26f09.jpg", 750m, 12, null },
                    { new Guid("fcf628c8-44c0-40ed-b042-28367de2c0ad"), new Guid("f253dbfb-1b3d-45a0-90f7-463f7383e20c"), new DateTime(2023, 1, 2, 22, 3, 49, 26, DateTimeKind.Local).AddTicks(8129), "Dyson V15 Detect Absolute Cordless Vacuum Cleaner 60 Mins Run 2 Year", "Dyson V15 Detect", "b44fd94f-12ff-4b4d-acdb-e0db37219bae.jpg", 600m, 10, null }
                });

            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] { "Id", "Color", "Height", "Weight", "Width" },
                values: new object[,]
                {
                    { new Guid("1c32da1d-8b13-418b-bd78-5fe483bef59c"), "White", "14'", "1.8 kg", "15.3'" },
                    { new Guid("46ba7244-7d95-4274-bfba-ca374023d303"), "Purple", "6.33'", "0.25 kg", "3.07'" },
                    { new Guid("a738ca5c-cf72-4faf-9f50-68037fb3f4d5"), "White", "2.2'", "2.9 kg", "12.6'" },
                    { new Guid("c44ee253-20d5-4e19-9055-d661b9be5703"), "Black", "12'", "2.5 kg", "15.3'" },
                    { new Guid("d6a4d2c5-9087-4278-a161-f96a142eb606"), "Black", "7.2'", "0.40 kg", "3.6'" },
                    { new Guid("fcf628c8-44c0-40ed-b042-28367de2c0ad"), "Silver", "39.2'", "4.9 kg", "6.6'" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
