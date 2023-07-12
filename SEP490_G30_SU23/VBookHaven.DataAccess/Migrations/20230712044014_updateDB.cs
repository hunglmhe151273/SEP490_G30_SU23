using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBookHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShippingInfo",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ShippingInfoId",
                table: "Order",
                newName: "ShippingInfoShipInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ShippingInfoId",
                table: "Order",
                newName: "IX_Order_ShippingInfoShipInfoId");

            migrationBuilder.AddColumn<string>(
                name: "ShippingInfo",
                table: "Order",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultShippingInfoId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DefaultShippingInfoId",
                table: "Customer",
                column: "DefaultShippingInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_ShippingInfo",
                table: "Customer",
                column: "DefaultShippingInfoId",
                principalTable: "ShippingInfo",
                principalColumn: "ShipInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShippingInfo_ShippingInfoShipInfoId",
                table: "Order",
                column: "ShippingInfoShipInfoId",
                principalTable: "ShippingInfo",
                principalColumn: "ShipInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ShippingInfo",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShippingInfo_ShippingInfoShipInfoId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Customer_DefaultShippingInfoId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ShippingInfo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DefaultShippingInfoId",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "ShippingInfoShipInfoId",
                table: "Order",
                newName: "ShippingInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ShippingInfoShipInfoId",
                table: "Order",
                newName: "IX_Order_ShippingInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShippingInfo",
                table: "Order",
                column: "ShippingInfoId",
                principalTable: "ShippingInfo",
                principalColumn: "ShipInfoId");
        }
    }
}
