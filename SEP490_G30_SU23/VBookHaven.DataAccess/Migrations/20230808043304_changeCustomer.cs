using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBookHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Customer",
                table: "Customer",
                column: "AccountId",
                unique: true,
                filter: "AccountId IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Customer",
                table: "Customer",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");
        }
    }
}
