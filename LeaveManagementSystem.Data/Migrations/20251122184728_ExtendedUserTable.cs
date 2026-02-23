using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9f7bf52-b796-43e1-8771-3384194ab576",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a145ef3-d228-4999-85b6-9efc515fe2b0", new DateOnly(1995, 3, 15), "Default", "Admin", "AQAAAAIAAYagAAAAEM3NlyIJ3QHOGpLsWBj9OHZ4ogJ7WuuOndH6vsZ/2sCgRm6CHmqh8BN2gEo1Ys9qzA==", "8672f579-45df-4d13-85fa-7f73b0fda6a6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9f7bf52-b796-43e1-8771-3384194ab576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "95cf0871-9cf9-49c0-86c8-826cae49995b", "AQAAAAIAAYagAAAAEOM12bCUpD4qYwmZWYmU49vH368yYz8WtBSqFs1Tzc7So2JNBNBG2LoOtpHFzTcppw==", "eedea565-ec17-46d2-9d92-5c26968ce6a7" });
        }
    }
}
