using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementTeam6.Data.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 21, 8, 6, 52, 677, DateTimeKind.Utc).AddTicks(1111));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsFirst", "JoinedDate" },
                values: new object[] { false, new DateTime(2022, 11, 21, 8, 6, 52, 677, DateTimeKind.Utc).AddTicks(1113) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 21, 8, 6, 52, 677, DateTimeKind.Utc).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 21, 8, 6, 52, 677, DateTimeKind.Utc).AddTicks(1118));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 21, 8, 6, 22, 983, DateTimeKind.Utc).AddTicks(2755));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsFirst", "JoinedDate" },
                values: new object[] { true, new DateTime(2022, 11, 21, 8, 6, 22, 983, DateTimeKind.Utc).AddTicks(2757) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 21, 8, 6, 22, 983, DateTimeKind.Utc).AddTicks(2759));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 21, 8, 6, 22, 983, DateTimeKind.Utc).AddTicks(2761));
        }
    }
}
