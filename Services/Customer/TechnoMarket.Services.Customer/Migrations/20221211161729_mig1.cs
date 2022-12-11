using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnoMarket.Services.Customer.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    AddressAddressLine = table.Column<string>(name: "Address_AddressLine", type: "varchar", maxLength: 255, nullable: false),
                    AddressCity = table.Column<string>(name: "Address_City", type: "varchar", maxLength: 100, nullable: false),
                    AddressCountry = table.Column<string>(name: "Address_Country", type: "varchar", maxLength: 100, nullable: false),
                    AddressCityCode = table.Column<short>(name: "Address_CityCode", type: "smallint", maxLength: 81, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
