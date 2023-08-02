using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBookHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatePPHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "PurchasePaymentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePaymentHistory_StaffId",
                table: "PurchasePaymentHistory",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePaymentHistory_Staff",
                table: "PurchasePaymentHistory",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePaymentHistory_Staff",
                table: "PurchasePaymentHistory");

            migrationBuilder.DropIndex(
                name: "IX_PurchasePaymentHistory_StaffId",
                table: "PurchasePaymentHistory");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "PurchasePaymentHistory");
        }
    }
}
