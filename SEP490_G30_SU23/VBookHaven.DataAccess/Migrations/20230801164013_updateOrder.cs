using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBookHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountPaid",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VAT",
                table: "Order",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_StaffId",
                table: "Order",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Staff",
                table: "Order",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Staff",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_StaffId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Order");
        }
    }
}
