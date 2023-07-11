using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBookHaven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateStaffPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Staff",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(15)",
                oldFixedLength: true,
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsMale",
                table: "Staff",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdCard",
                table: "Staff",
                type: "nchar(12)",
                fixedLength: true,
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(12)",
                oldFixedLength: true,
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Staff",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Staff",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Staff",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Staff",
                type: "nchar(15)",
                fixedLength: true,
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<bool>(
                name: "IsMale",
                table: "Staff",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "IdCard",
                table: "Staff",
                type: "nchar(12)",
                fixedLength: true,
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(12)",
                oldFixedLength: true,
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Staff",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Staff",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Staff",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
