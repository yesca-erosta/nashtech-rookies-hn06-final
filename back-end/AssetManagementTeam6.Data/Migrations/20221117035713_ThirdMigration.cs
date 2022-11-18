using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementTeam6.Data.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "FirstName", "Gender", "JoinedDate", "LastName", "StaffCode" },
                values: new object[] { new DateTime(2000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dong", 1, new DateTime(2022, 11, 17, 3, 57, 12, 675, DateTimeKind.Utc).AddTicks(4758), "Nguyen", "SD0001" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateOfBirth", "FirstName", "Gender", "JoinedDate", "LastName", "StaffCode" },
                values: new object[] { new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoan", 1, new DateTime(2022, 11, 17, 3, 57, 12, 675, DateTimeKind.Utc).AddTicks(4764), "Nguyen", "SD0002" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "JoinedDate", "LastName", "Password", "StaffCode", "Type", "Username" },
                values: new object[,]
                {
                    { 3, new DateTime(1988, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Duc", 1, new DateTime(2022, 11, 17, 3, 57, 12, 675, DateTimeKind.Utc).AddTicks(4766), "Bui", "123456", "SD0003", 1, "ducbh" },
                    { 4, new DateTime(2003, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hang", 2, new DateTime(2022, 11, 17, 3, 57, 12, 675, DateTimeKind.Utc).AddTicks(4768), "Le", "123456", "SD0004", 0, "hanglt" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "FirstName", "Gender", "JoinedDate", "LastName", "StaffCode" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateOfBirth", "FirstName", "Gender", "JoinedDate", "LastName", "StaffCode" },
                values: new object[] { null, null, null, null, null, null });
        }
    }
}
