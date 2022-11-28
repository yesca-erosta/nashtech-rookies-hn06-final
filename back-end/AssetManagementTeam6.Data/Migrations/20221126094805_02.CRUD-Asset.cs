using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementTeam6.Data.Migrations
{
    public partial class _02CRUDAsset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 26, 9, 48, 5, 561, DateTimeKind.Utc).AddTicks(3785));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 26, 9, 48, 5, 561, DateTimeKind.Utc).AddTicks(3787));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 26, 9, 48, 5, 561, DateTimeKind.Utc).AddTicks(3789));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 26, 9, 41, 55, 187, DateTimeKind.Utc).AddTicks(871));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 26, 9, 41, 55, 187, DateTimeKind.Utc).AddTicks(874));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinedDate",
                value: new DateTime(2022, 11, 26, 9, 41, 55, 187, DateTimeKind.Utc).AddTicks(875));
        }
    }
}
