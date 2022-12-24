using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnoMarket.Services.Customer.Migrations
{
    public partial class migV01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    FirstName = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Address_AddressLine = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Address_City = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Address_Country = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Address_CityCode = table.Column<short>(type: "int2", maxLength: 81, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
