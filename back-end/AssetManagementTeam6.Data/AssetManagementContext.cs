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

            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User { Id = 1, StaffCode = "SD0001", Username = "dongnp", Password = "123456", FirstName = "Dong", LastName = "Nguyen", DateOfBirth = new DateTime(2000, 01, 13), Gender = GenderEnum.Male, Type = StaffEnum.Admin, JoinedDate = DateTime.UtcNow, Location=LocationEnum.HN, IsFirst = false },
                new User { Id = 2, StaffCode = "SD0002", Username = "hoannv", Password = "123456", FirstName = "Hoan", LastName = "Nguyen", DateOfBirth = new DateTime(2000, 11, 03), Gender = GenderEnum.Male, Type = StaffEnum.Admin, JoinedDate = DateTime.UtcNow, Location = LocationEnum.HCM, IsFirst = false },
                new User { Id = 3, StaffCode = "SD0003", Username = "ducbh", Password = "123456", FirstName = "Duc", LastName = "Bui", DateOfBirth = new DateTime(1988, 11, 20), Gender = GenderEnum.Male, Type = StaffEnum.Staff, JoinedDate = DateTime.UtcNow, Location = LocationEnum.HN, IsFirst = true },
                new User { Id = 4, StaffCode = "SD0004", Username = "hanglt", Password = "123456", FirstName = "Hang", LastName = "Le", DateOfBirth = new DateTime(2003, 11, 02), Gender = GenderEnum.Female, Type = StaffEnum.Staff, JoinedDate = DateTime.UtcNow, Location = LocationEnum.HN, IsFirst = true }
               );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<RequestForReturn> RequestsForReturn { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
