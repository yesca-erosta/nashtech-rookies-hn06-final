using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
using Common.Enums;
using Moq;
using System.Linq.Expressions;

namespace AssetManagementTeam6.API.Test.Services
{
    public class UserServiceTest
    {
        private Mock<IUserRepository> _mockUserRepository;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
        }

        public User GetSampleUser()
        {
            return new User
            {
                Id = 1,
                UserName = "dongnp13",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = SystemFunction.CreateMD5("Admin@123"),
                Type = StaffEnum.Admin,
            };
        }

        public UserRequest ConvertToUserRequest(User user)
        {
            return new UserRequest
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                JoinedDate = user.JoinedDate,
                Location = user.Location,
                NeedUpdatePwdOnLogin = user.NeedUpdatePwdOnLogin,
                Password = user.Password,
                Type = user.Type
            };
        }

        public IEnumerable<User> GetSampleUserLists()
        {
            return new List<User>
            {
                new User {
                    Id = 1,
                    //StaffCode = "SD0001",
                    FirstName = "Dong",
                    LastName = "Nguyen Phuong",
                    UserName = "dongnp13",
                    DateOfBirth = new DateTime(2000,01,01),
                    Password = SystemFunction.CreateMD5("Admin@123"),
                    JoinedDate = new DateTime(2022,01,01),
                    Type = StaffEnum.Admin,
                    Gender = GenderEnum.Male,
                    Location = LocationEnum.HN,
                    NeedUpdatePwdOnLogin = false,
                },
                new User {
                    Id = 2,
                    //StaffCode = "SD0001",
                    FirstName = "Hoan",
                    LastName = "Nguyen Van",
                    UserName = "hoannv",
                    DateOfBirth = new DateTime(2001,11,01),
                    Password = SystemFunction.CreateMD5("Admin@1234"),
                    JoinedDate = new DateTime(2012,01,01),
                    Type = StaffEnum.Admin,
                    Gender = GenderEnum.Male,
                    Location = LocationEnum.DN,
                    NeedUpdatePwdOnLogin = true,
                },
                 new User {
                    Id = 3,
                    //StaffCode = "SD0001",
                    FirstName = "Tai",
                    LastName = "Pham Tien",
                    UserName = "taitienpham",
                    DateOfBirth = new DateTime(2000,05,03),
                    Password = SystemFunction.CreateMD5("Tientai@2k"),
                    JoinedDate = new DateTime(2010,02,02),
                    Type = StaffEnum.Staff,
                    Gender = GenderEnum.Male,
                    Location = LocationEnum.HN,
                    NeedUpdatePwdOnLogin = false,
                },
                  new User {
                    Id = 4,
                    //StaffCode = "SD0001",
                    FirstName = "Tien",
                    LastName = "Nguyen Ngoc",
                    UserName = "tiennguyen99",
                    DateOfBirth = new DateTime(1999,09,05),
                    Password = SystemFunction.CreateMD5("TienTien@123"),
                    JoinedDate = new DateTime(2005,10,12),
                    Type = StaffEnum.Admin,
                    Gender = GenderEnum.Male,
                    Location = LocationEnum.HCM,
                    NeedUpdatePwdOnLogin = true,
                },
            };
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull()
        {
            //Arrange
            User? user = null;
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserById(1);

            //Assert
            Assert.Null(testResult);

        }

        [Fact]
        public async Task GetUserById_ShouldNotReturnNull()
        {
            //Arrange
            var user = GetSampleUser();

            var expectedResult = new User
            {
                Id = 1,
                UserName = "dongnp13",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = "0E7517141FB53F21EE439B355B5A1D0A",
                Type = StaffEnum.Admin,
            };

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserById(1);

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(expectedResult.Id, testResult!.Id);
            Assert.Equal(expectedResult.UserName, testResult!.UserName);
            Assert.Equal(expectedResult.FirstName, testResult!.FirstName);
            Assert.Equal(expectedResult.LastName, testResult!.LastName);
            Assert.Equal(expectedResult.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(expectedResult.Gender, testResult!.Gender);
            Assert.Equal(expectedResult.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(expectedResult.Location, testResult!.Location);
            Assert.Equal(expectedResult.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(expectedResult.Password, testResult!.Password);
            Assert.Equal(expectedResult.Type, testResult!.Type);
        }

        [Fact]
        public async Task GetUserByStaffCode_ShouldReturnNull()
        {
            //Arrange
            User? user = null;
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserByStaffCode("SD0001");

            //Assert
            Assert.Null(testResult);

        }

        [Fact]
        public async Task GetUserByStaffCode_ShouldNotReturnNull()
        {
            //Arrange
            var user = new User
            {
                Id = 1,
                UserName = "dongnp13",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = "0E7517141FB53F21EE439B355B5A1D0A",
                Type = StaffEnum.Admin,
            };

            var expectedResult = new User
            {
                Id = 1,
                UserName = "dongnp13",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = "0E7517141FB53F21EE439B355B5A1D0A",
                Type = StaffEnum.Admin,
            };

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserByStaffCode("SD0001");

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(expectedResult.Id, testResult!.Id);
            Assert.Equal(expectedResult.UserName, testResult!.UserName);
            Assert.Equal(expectedResult.FirstName, testResult!.FirstName);
            Assert.Equal(expectedResult.LastName, testResult!.LastName);
            Assert.Equal(expectedResult.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(expectedResult.Gender, testResult!.Gender);
            Assert.Equal(expectedResult.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(expectedResult.Location, testResult!.Location);
            Assert.Equal(expectedResult.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(expectedResult.Password, testResult!.Password);
            Assert.Equal(expectedResult.Type, testResult!.Type);
        }
        [Fact]
        public async Task LoginUser_ShouldReturnNull()
        {
            //Arrange
            User? user = null;
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.LoginUser(new LoginRequest { Password = "" });

            //Assert
            Assert.Null(testResult);

        }

        [Theory]
        [InlineData(StaffEnum.Staff, "dongnp", true, LocationEnum.HN)]
        [InlineData(StaffEnum.Admin, "dongnp13", true, LocationEnum.HN)]
        [InlineData(StaffEnum.Staff, "dongnp", true, LocationEnum.HCM)]
        [InlineData(StaffEnum.Admin, "dongnp13", true, LocationEnum.DN)]
        [InlineData(StaffEnum.Staff, "dongnp", false, LocationEnum.HCM)]
        [InlineData(StaffEnum.Admin, "dongnp13", false, LocationEnum.DN)]
        public async Task LoginUser_ShouldNotReturnNull(StaffEnum userType, string userName, bool needUpdatePwdOnLogin, LocationEnum location)
        {
            //Arrange
            var user = new User
            {
                Id = 1,
                UserName = userName,
                Type = userType,
                NeedUpdatePwdOnLogin = needUpdatePwdOnLogin,
                Location = location,
                Password = "Admin@123"
            };

            var expectedResult = new LoginResponse
            {
                Id = 1,
                UserName = userName,
                Type = userType,
                NeedUpdatePwdOnLogin = needUpdatePwdOnLogin,
                Location = location,
            };

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.LoginUser(new LoginRequest { UserName = user.UserName, Password = user.Password });

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(expectedResult.Id, testResult!.Id);
            Assert.Equal(expectedResult.UserName, testResult!.UserName);
            Assert.Equal(expectedResult.Type, testResult!.Type);
            Assert.Equal(expectedResult.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(expectedResult.Location, testResult!.Location);
        }

        [Fact]
        public async Task GetUserByUserName_ShouldReturnNull()
        {
            //Arrange
            User? user = null;
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserByUserAccount("dongnp13");

            //Assert
            Assert.Null(testResult);

        }

        [Fact]
        public async Task GetUserByUserName_ShouldNotReturnNull()
        {
            //Arrange
            var user = GetSampleUser();

            var expectedResult = new User
            {
                Id = 1,
                UserName = "dongnp13",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = "0E7517141FB53F21EE439B355B5A1D0A",
                Type = StaffEnum.Admin,
            };

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserByUserAccount("dongnp13");

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(expectedResult.Id, testResult!.Id);
            Assert.Equal(expectedResult.UserName, testResult!.UserName);
            Assert.Equal(expectedResult.FirstName, testResult!.FirstName);
            Assert.Equal(expectedResult.LastName, testResult!.LastName);
            Assert.Equal(expectedResult.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(expectedResult.Gender, testResult!.Gender);
            Assert.Equal(expectedResult.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(expectedResult.Location, testResult!.Location);
            Assert.Equal(expectedResult.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(expectedResult.Password, testResult!.Password);
            Assert.Equal(expectedResult.Type, testResult!.Type);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnNull()
        {
            //Arrange
            User? user = null;
            _mockUserRepository.Setup(x => x.Update(user)).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.ChangePassword(user);

            //Assert
            Assert.Null(testResult);

        }

        [Fact]
        public async Task ChangePassword_ShouldNotReturnNull()
        {
            //Arrange
            var user = GetSampleUser();

            var expectedResult = new User
            {
                Id = 1,
                UserName = "dongnp13",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = "0E7517141FB53F21EE439B355B5A1D0A",
                Type = StaffEnum.Admin,
            };

            _mockUserRepository.Setup(x => x.Update(user)).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.ChangePassword(user);

            //Assert
            Assert.NotNull(testResult);
            Assert.IsType<User>(testResult);
            Assert.Equal(expectedResult.Id, testResult!.Id);
            Assert.Equal(expectedResult.UserName, testResult!.UserName);
            Assert.Equal(expectedResult.FirstName, testResult!.FirstName);
            Assert.Equal(expectedResult.LastName, testResult!.LastName);
            Assert.Equal(expectedResult.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(expectedResult.Gender, testResult!.Gender);
            Assert.Equal(expectedResult.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(expectedResult.Location, testResult!.Location);
            Assert.Equal(expectedResult.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(expectedResult.Password, testResult!.Password);
            Assert.Equal(expectedResult.Type, testResult!.Type);
        }

        //TODO: Handling
        [Fact]
        public async Task CreateNewUser_ShouldNotReturnNull()
        {
            //Arrange
            var user = new User
            {
                Id = 100,
                UserName = "dongnp130120001",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = "Admin@123",
                Type = StaffEnum.Admin,
            };

            var convertUser = ConvertToUserRequest(user);

            var expectedResult = new User
            {
                Id = 100,
                UserName = "dongnp130120001",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = null,
                Type = StaffEnum.Admin,
            };

            _mockUserRepository.Setup(x => x.Create(user)).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.Create(convertUser);

            //Assert
            Assert.NotNull(testResult);
            Assert.IsType<User>(testResult);
            Assert.Equal(expectedResult.Id, testResult!.Id);
            Assert.Equal(expectedResult.UserName, testResult!.UserName);
            Assert.Equal(expectedResult.FirstName, testResult!.FirstName);
            Assert.Equal(expectedResult.LastName, testResult!.LastName);
            Assert.Equal(expectedResult.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(expectedResult.Gender, testResult!.Gender);
            Assert.Equal(expectedResult.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(expectedResult.Location, testResult!.Location);
            Assert.Equal(expectedResult.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(expectedResult.Password, testResult!.Password);
            Assert.Equal(expectedResult.Type, testResult!.Type);
        }

        //TODO: Handling
        [Fact]
        // [InlineData("dong", "Dong", "Nguyen Phuong", new DateTime(2000, 01, 13), new DateTime(2010, 10, 10))]
        //string userName, string firstName, string lastName, DateTime dob, DateTime joinedDate
        public async Task CreateNewUser_ShouldReturnNull()
        {
            //Arrange
            User? user = null;
            var convertUser = null as UserRequest;

            _mockUserRepository.Setup(x => x.Create(user)).ReturnsAsync(null as User);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.Create(convertUser);

            //Assert
            Assert.Null(testResult);
        }

        //TODO: Handling
        [Theory]
        [InlineData(LocationEnum.HN)]
        [InlineData(LocationEnum.DN)]
        [InlineData(LocationEnum.HCM)]
        public async Task GetListOfUsers_ReturnListOfUsers(LocationEnum location)
        {
            // Arrange
            var users = GetSampleUserLists();
            _mockUserRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users);
            var userService = new UserService(_mockUserRepository.Object);

            // Act
            var testResult = await userService.GetAllAsync(location) as IEnumerable<GetUserResponse>;

            // Assert
            //Assert.IsType<List<GetUserResponse>>(testResult);
            //Assert.Equal(GetSampleUserLists().Count(), testResult.Count());
            Assert.NotNull(testResult);
        }

        //TODO: Handling
        [Theory]
        [InlineData(LocationEnum.HN)]
        [InlineData(LocationEnum.DN)]
        [InlineData(LocationEnum.HCM)]
        public async Task EmptyUserList_ReturnListOfNone(LocationEnum location)
        {
            // Arrange
            var users = new List<User>();
            _mockUserRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users);
            var service = new UserService(_mockUserRepository.Object);

            // Act
            var testResult = await service.GetAllAsync(location);

            // Assert
            //Assert.IsType<List<GetUserResponse>>(testResult);
            Assert.Equal(0, testResult.Count());
            Assert.NotNull(testResult);

        }

    }
}
