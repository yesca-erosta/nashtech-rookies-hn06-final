﻿// <auto-generated />
using System;
using AssetManagementTeam6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetManagementTeam6.Data.Migrations
{
    [DbContext(typeof(AssetManagementContext))]
    partial class AssetManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AssetManagementTeam6.Data.Entities.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InstalledDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Specification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("AssetManagementTeam6.Data.Entities.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AssignedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AssignedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("AssignedTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("AssetManagementTeam6.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("NameCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("AssetManagementTeam6.Data.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Assigned")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Available")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("NotAvailable")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Recycled")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Total")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("WaitingForRecycling")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("AssetManagementTeam6.Data.Entities.RequestForReturn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AcceptedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AssignedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReturnedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RequestsForReturn");
                });

            modelBuilder.Entity("AssetManagementTeam6.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime?>("JoinedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(2000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Dong",
                            Gender = 1,
                            JoinedDate = new DateTime(2022, 11, 18, 6, 58, 26, 393, DateTimeKind.Utc).AddTicks(2303),
                            LastName = "Nguyen",
                            Password = "123456",
                            StaffCode = "SD0001",
                            Type = 0,
                            Username = "dongnp"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Hoan",
                            Gender = 1,
                            JoinedDate = new DateTime(2022, 11, 18, 6, 58, 26, 393, DateTimeKind.Utc).AddTicks(2305),
                            LastName = "Nguyen",
                            Password = "123456",
                            StaffCode = "SD0002",
                            Type = 0,
                            Username = "hoannv"
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1988, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Duc",
                            Gender = 1,
                            JoinedDate = new DateTime(2022, 11, 18, 6, 58, 26, 393, DateTimeKind.Utc).AddTicks(2306),
                            LastName = "Bui",
                            Password = "123456",
                            StaffCode = "SD0003",
                            Type = 1,
                            Username = "ducbh"
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(2003, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Hang",
                            Gender = 2,
                            JoinedDate = new DateTime(2022, 11, 18, 6, 58, 26, 393, DateTimeKind.Utc).AddTicks(2309),
                            LastName = "Le",
                            Password = "123456",
                            StaffCode = "SD0004",
                            Type = 0,
                            Username = "hanglt"
                        });
                });

            modelBuilder.Entity("AssetManagementTeam6.Data.Entities.Asset", b =>
                {
                    b.HasOne("AssetManagementTeam6.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AssetManagementTeam6.Data.Entities.Report", b =>
                {
                    b.HasOne("AssetManagementTeam6.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
