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
            builder.Entity<Asset>().HasData(
                new Asset
                {
                    Id = 1,
                    AssetName = "Laptop1",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G301",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                },
                new Asset
                {
                    Id = 2,
                    AssetName = "Laptop2",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G302",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                },
                new Asset
                {
                    Id = 3,
                    AssetName = "Laptop3",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G303",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                },
                new Asset
                {
                    Id = 4,
                    AssetName = "Laptop4",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G304",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                },
                new Asset
                {
                    Id = 5,
                    AssetName = "Laptop5",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G305",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                }, new Asset
                {
                    Id = 6,
                    AssetName = "Laptop6",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G306",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                }, new Asset
                {
                    Id = 7,
                    AssetName = "Laptop7",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G307",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                }, new Asset
                {
                    Id = 8,
                    AssetName = "Laptop8",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G308",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                }, new Asset
                {
                    Id = 9,
                    AssetName = "Laptop9",
                    CategoryId = "LA",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Laptop G309",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                }, new Asset
                {
                    Id = 10,
                    AssetName = "Monitor",
                    CategoryId = "MO",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Monitor M300",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                },
                new Asset
                {
                    Id = 11,
                    AssetName = "Monitor1",
                    CategoryId = "MO",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Monitor M301",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                },
                 new Asset
                 {
                     Id = 12,
                     AssetName = "Monitor2",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M302",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 }
                , new Asset
                {
                    Id = 13,
                    AssetName = "Monitor3",
                    CategoryId = "MO",
                    InstalledDate = DateTime.UtcNow,
                    Specification = "Monitor M303",
                    Location = LocationEnum.HN,
                    State = AssetStateEnum.Assigned
                }
                ,
                 new Asset
                 {
                     Id = 14,
                     AssetName = "Monitor4",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M304",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 },
                 new Asset
                 {
                     Id = 15,
                     AssetName = "Monitor5",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M305",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 },
                 new Asset
                 {
                     Id = 16,
                     AssetName = "Monitor6",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M306",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 },
                 new Asset
                 {
                     Id = 17,
                     AssetName = "Monitor7",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M307",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 },
                 new Asset
                 {
                     Id = 18,
                     AssetName = "Monitor8",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M308",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 },
                 new Asset
                 {
                     Id = 19,
                     AssetName = "Monitor9",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M309",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 },
                 new Asset
                 {
                     Id = 20,
                     AssetName = "Monitor10",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M310",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 },
                 new Asset
                 {
                     Id = 21,
                     AssetName = "Monitor11",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M311",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 },
                 new Asset
                 {
                     Id = 22,
                     AssetName = "Monitor12",
                     CategoryId = "MO",
                     InstalledDate = DateTime.UtcNow,
                     Specification = "Monitor M312",
                     Location = LocationEnum.HN,
                     State = AssetStateEnum.Assigned
                 }


                );
            builder.Entity<Assignment>().HasData(
                 new Assignment
                 {
                     Id = 1,
                     AssetId = 1,
                     AssignedById = 1,
                     AssignedDate = DateTime.UtcNow,
                     AssignedToId = 2,
                     Note = "Assignment 1",
                     State = AssignmentStateEnum.Accepted
                 },
                new Assignment
                {
                    Id = 2,
                    AssetId = 2,
                    AssignedById = 1,
                    AssignedDate = DateTime.UtcNow,
                    AssignedToId = 3,
                    Note = "Assignment 2",
                    State = AssignmentStateEnum.WaitingForAcceptance
                },
                new Assignment
                {
                    Id = 3,
                    AssetId = 3,
                    AssignedById = 1,
                    AssignedDate = DateTime.UtcNow,
                    AssignedToId = 3,
                    Note = "Assignment 3",
                    State = AssignmentStateEnum.Accepted
                },
                new Assignment
                {
                    Id = 4,
                    AssetId = 2,
                    AssignedById = 1,
                    AssignedDate = DateTime.UtcNow,
                    AssignedToId = 2,
                    Note = "Assignment 4",
                    State = AssignmentStateEnum.WaitingForAcceptance
                }
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<RequestForReturning> RequestsForReturns { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
