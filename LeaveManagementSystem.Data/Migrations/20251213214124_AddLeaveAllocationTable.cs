using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveAllocationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9f7bf52-b796-43e1-8771-3384194ab576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd9508ed-c559-4bad-9521-923bcf7762cc", "AQAAAAIAAYagAAAAEH81X7xkkWXutRObhSgxPIIrO98wRu9Pc04yWwUqX48zzMiuWCyH1MwSSQFg8dVszQ==", "5d5a082d-020c-431d-a737-f8b637078721" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_PeriodId",
                table: "LeaveAllocations",
                column: "PeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveAllocations");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9f7bf52-b796-43e1-8771-3384194ab576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a145ef3-d228-4999-85b6-9efc515fe2b0", "AQAAAAIAAYagAAAAEM3NlyIJ3QHOGpLsWBj9OHZ4ogJ7WuuOndH6vsZ/2sCgRm6CHmqh8BN2gEo1Ys9qzA==", "8672f579-45df-4d13-85fa-7f73b0fda6a6" });
        }
    }
}
