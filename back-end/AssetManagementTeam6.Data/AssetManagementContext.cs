using AssetManagementTeam6.Data.Entities;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementTeam6.Data
{
    public class AssetManagementContext : DbContext
    {
        public AssetManagementContext(DbContextOptions<AssetManagementContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            builder.Entity<User>()
                .Property(u => u.StaffCode)
                .HasComputedColumnSql("N'SD'+ RIGHT('0000'+CAST(Id AS VARCHAR(4)),4)");

            builder.Entity<Asset>()
                .Property(c => c.AssetCode)
                .HasComputedColumnSql("[CategoryId]+ RIGHT('000000'+CAST(Id AS VARCHAR(6)),6)");

            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Ha Noi",
                    DateOfBirth = new DateTime(2000, 01, 13),
                    Gender = GenderEnum.Male,
                    Type = StaffEnum.Admin,
                    JoinedDate = DateTime.UtcNow,
                    Location = LocationEnum.HN,
                    NeedUpdatePwdOnLogin = false,
                    Password = "0E7517141FB53F21EE439B355B5A1D0A",
                    UserName = "adminhn"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Admin",
                    LastName = "HCMC",
                    DateOfBirth = new DateTime(2000, 01, 13),
                    Gender = GenderEnum.Male,
                    Type = StaffEnum.Admin,
                    JoinedDate = DateTime.UtcNow,
                    Location = LocationEnum.HCM,
                    NeedUpdatePwdOnLogin = false,
                    Password = "0E7517141FB53F21EE439B355B5A1D0A",
                    UserName = "adminhcmc"
                },
                new User
                {
                    Id = 3,
                    FirstName = "Admin",
                    LastName = "Da Nang",
                    DateOfBirth = new DateTime(2000, 01, 13),
                    Gender = GenderEnum.Male,
                    Type = StaffEnum.Admin,
                    JoinedDate = DateTime.UtcNow,
                    Location = LocationEnum.DN,
                    NeedUpdatePwdOnLogin = false,
                    Password = "0E7517141FB53F21EE439B355B5A1D0A",
                    UserName = "admindn"
                }
            );

            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = "LA",
                    Name = "Laptop"
                },
                new Category
                {
                    Id = "MO",
                    Name = "Mornitor"
                },
                new Category
                {
                    Id = "PC",
                    Name = "Personal computer"
                }
            );
            for (int i = 1; i <= 25; i = i + 3)
            {
                builder.Entity<Asset>().HasData(
                    new Asset
                    {
                        Id = i,
                        AssetName = $"Asset{i}",
                        CategoryId = "LA",
                        InstalledDate = DateTime.UtcNow,
                        Specification = $"Laptop {i.ToString("D3")}",
                        Location = (LocationEnum)(i % 3),
                        State = (i % 2 == 0 ? AssetStateEnum.Available : AssetStateEnum.Assigned)
                    }
                );
            }
            for (int i = 2; i <= 25; i = i + 3)
            {
                builder.Entity<Asset>().HasData(
                    new Asset
                    {
                        Id = i,
                        AssetName = $"Asset{i}",
                        CategoryId = "MO",
                        InstalledDate = DateTime.UtcNow,
                        Specification = $"Monitor {i.ToString("D3")}",
                        Location = (LocationEnum)(i % 2),
                        State = (i % 2 == 0 ? AssetStateEnum.Available : AssetStateEnum.Assigned)
                    }
                );
            }
            for (int i = 3; i <= 25; i = i + 3)
            {
                builder.Entity<Asset>().HasData(
                    new Asset
                    {
                        Id = i,
                        AssetName = $"Asset{i}",
                        CategoryId = "PC",
                        InstalledDate = DateTime.UtcNow,
                        Specification = $"Personal Computer {i.ToString("D3")}",
                        Location = (LocationEnum)(i % 3),
                        State = (i % 2 == 0 ? AssetStateEnum.Available : AssetStateEnum.Assigned)
                    }
                );
            }

            for (int i = 1; i <= 4; i++)
            {
                builder.Entity<Assignment>().HasData(
                     new Assignment
                     {
                         Id = i,
                         AssetId = i,
                         AssignedById = 1,
                         AssignedDate = DateTime.UtcNow,
                         AssignedToId = 1,
                         Note = $"Assignment {i}",
                         State = (AssignmentStateEnum)(i % 2)
                     }
                 );
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<RequestForReturning> RequestsForReturns { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
