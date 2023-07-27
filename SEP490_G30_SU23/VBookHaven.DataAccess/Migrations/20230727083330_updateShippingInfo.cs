using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBookHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateShippingInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "ShippingInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictCode",
                table: "ShippingInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "ShippingInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceCode",
                table: "ShippingInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "ShippingInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WardCode",
                table: "ShippingInfo",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "ShippingInfo");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "ShippingInfo");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "ShippingInfo");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "ShippingInfo");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "ShippingInfo");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "ShippingInfo");
        }
    }
}
