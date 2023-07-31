using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBookHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeOrderShipInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingInfo",
                table: "Order",
                newName: "ShipAddress");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Order",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Order",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ShipAddress",
                table: "Order",
                newName: "ShippingInfo");
        }
    }
}
