using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultRolesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11bddaad-739a-4cbe-9b31-72d77622c5df", null, "Employee", "EMPLOYEE" },
                    { "29623920-e2f4-4738-ac01-c2ec3c3bdc41", null, "Supervisor", "SUPERVISOR" },
                    { "60dc1a77-b586-447d-8a88-7077c250911c", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e9f7bf52-b796-43e1-8771-3384194ab576", 0, "95cf0871-9cf9-49c0-86c8-826cae49995b", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEOM12bCUpD4qYwmZWYmU49vH368yYz8WtBSqFs1Tzc7So2JNBNBG2LoOtpHFzTcppw==", null, false, "eedea565-ec17-46d2-9d92-5c26968ce6a7", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "60dc1a77-b586-447d-8a88-7077c250911c", "e9f7bf52-b796-43e1-8771-3384194ab576" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11bddaad-739a-4cbe-9b31-72d77622c5df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29623920-e2f4-4738-ac01-c2ec3c3bdc41");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "60dc1a77-b586-447d-8a88-7077c250911c", "e9f7bf52-b796-43e1-8771-3384194ab576" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60dc1a77-b586-447d-8a88-7077c250911c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9f7bf52-b796-43e1-8771-3384194ab576");
        }
    }
}
