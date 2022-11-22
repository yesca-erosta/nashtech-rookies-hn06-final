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

            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            //builder.Entity<User>().HasData(
                //new User { Id = 1, FirstName = "Dong", LastName = "Nguyen Phuong", DateOfBirth = new DateTime(2000, 01, 13), Gender = GenderEnum.Male, Type = StaffEnum.Admin, JoinedDate = DateTime.UtcNow, Location = LocationEnum.HN, IsFirst = false, },
                //new User { Id = 2, FirstName = "Hoan", LastName = "Nguyen Van", DateOfBirth = new DateTime(2000, 11, 03), Gender = GenderEnum.Male, Type = StaffEnum.Admin, JoinedDate = DateTime.UtcNow, Location = LocationEnum.HCM, IsFirst = false },
                //new User { Id = 3, FirstName = "Duc", LastName = "Bui Hong", DateOfBirth = new DateTime(1988, 11, 20), Gender = GenderEnum.Male, Type = StaffEnum.Staff, JoinedDate = DateTime.UtcNow, Location = LocationEnum.HN, IsFirst = true },
                //new User { Id = 4, FirstName = "Hang", LastName = "Le", DateOfBirth = new DateTime(2003, 11, 02), Gender = GenderEnum.Female, Type = StaffEnum.Staff, JoinedDate = DateTime.UtcNow, Location = LocationEnum.HN, IsFirst = true }
               //);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<RequestForReturn> RequestsForReturn { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
