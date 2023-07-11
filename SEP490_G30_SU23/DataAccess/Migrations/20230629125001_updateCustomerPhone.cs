using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBookHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateCustomerPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customer",
                type: "nchar(15)",
                fixedLength: true,
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customer",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(15)",
                oldFixedLength: true,
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}
