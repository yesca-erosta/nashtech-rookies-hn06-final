using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementTeam6.Data.Migrations
{
    public partial class _01UserAssetCategory202211222321 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestsForReturn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestsForReturn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    NeedUpdatePwdOnLogin = table.Column<bool>(type: "bit", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "[FirstName] + ' ' + [LastName]"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffCode = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "N'SD'+ RIGHT('0000'+CAST(Id AS VARCHAR(4)),4)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstalledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AssetCode = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "[CategoryId]+ RIGHT('000000'+CAST(Id AS VARCHAR(6)),6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Assigned = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<int>(type: "int", nullable: false),
                    NotAvailable = table.Column<int>(type: "int", nullable: false),
                    WaitingForRecycling = table.Column<int>(type: "int", nullable: false),
                    Recycled = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "LA", "Laptop" },
                    { "MO", "Mornitor" },
                    { "PC", "Personal computer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "JoinedDate", "LastName", "Location", "NeedUpdatePwdOnLogin", "Password", "Type", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 1, new DateTime(2022, 11, 22, 16, 21, 43, 272, DateTimeKind.Utc).AddTicks(8463), "Ha Noi", 0, false, "Admin@123", 1, "adminhn" },
                    { 2, new DateTime(2000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 1, new DateTime(2022, 11, 22, 16, 21, 43, 272, DateTimeKind.Utc).AddTicks(8465), "HCMC", 1, false, "Admin@123", 1, "adminhcmc" },
                    { 3, new DateTime(2000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 1, new DateTime(2022, 11, 22, 16, 21, 43, 272, DateTimeKind.Utc).AddTicks(8466), "Da Nang", 0, false, "Admin@123", 1, "admindn" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CategoryId",
                table: "Assets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CategoryId",
                table: "Reports",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "RequestsForReturn");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
