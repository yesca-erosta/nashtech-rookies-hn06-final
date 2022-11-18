using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementTeam6.Data.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 18, 6, 58, 26, 393, DateTimeKind.Utc).AddTicks(2303));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 18, 6, 58, 26, 393, DateTimeKind.Utc).AddTicks(2305));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 18, 6, 58, 26, 393, DateTimeKind.Utc).AddTicks(2306));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 18, 6, 58, 26, 393, DateTimeKind.Utc).AddTicks(2309));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 18, 6, 58, 0, 178, DateTimeKind.Utc).AddTicks(4541));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 18, 6, 58, 0, 178, DateTimeKind.Utc).AddTicks(4544));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 18, 6, 58, 0, 178, DateTimeKind.Utc).AddTicks(4545));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 18, 6, 58, 0, 178, DateTimeKind.Utc).AddTicks(4548));
        }
    }
}
