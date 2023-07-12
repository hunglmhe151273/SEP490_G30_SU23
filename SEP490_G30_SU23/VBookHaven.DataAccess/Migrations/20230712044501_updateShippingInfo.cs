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
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShippingInfo_ShippingInfoShipInfoId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShippingInfoShipInfoId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingInfoShipInfoId",
                table: "Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingInfoShipInfoId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShippingInfoShipInfoId",
                table: "Order",
                column: "ShippingInfoShipInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShippingInfo_ShippingInfoShipInfoId",
                table: "Order",
                column: "ShippingInfoShipInfoId",
                principalTable: "ShippingInfo",
                principalColumn: "ShipInfoId");
        }
    }
}
