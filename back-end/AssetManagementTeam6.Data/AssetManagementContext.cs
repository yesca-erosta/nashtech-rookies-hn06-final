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
                    Password = "Admin@123",
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
                    Password = "Admin@123",
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
                    Location = LocationEnum.HN,
                    NeedUpdatePwdOnLogin = false,
                    Password = "Admin@123",
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
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<RequestForReturn> RequestsForReturn { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
