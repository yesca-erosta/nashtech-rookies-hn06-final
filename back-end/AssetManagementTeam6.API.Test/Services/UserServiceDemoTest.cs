using AssetManagementTeam6.API.Dtos.Models;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Enums;
using Moq;
using System.Linq.Expressions;

namespace AssetManagementTeam6.API.Test.Services
{
    public class UserServiceDemoTest
    {
        private Mock<IUserRepository> _mockUserRepository;
        public UserServiceDemoTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull()
        {
            //Arrange
            User? userModel = null;
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(userModel);
            var userServiceDemo = new UserServiceDemo(_mockUserRepository.Object);

            //Act
            var testResult = await userServiceDemo.GetUserById(1);

            //Assert
            Assert.Null(testResult);

        }

        [Fact]
        public async Task GetUserById_ShouldNotReturnNull()
        {
            //Arrange
            var user = new User
            {
                Id = 1,
                Username = "JohnDoe"
            };

            var expectedResult = new UserModel
            {
                Id = 1,
                UserName = "JohnDoe"
            };

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userServiceDemo = new UserServiceDemo(_mockUserRepository.Object);

            //Act
            var testResult = await userServiceDemo.GetUserById(1);

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(expectedResult.Id, testResult!.Id);
            Assert.Equal(expectedResult.UserName, testResult!.UserName);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnNull()
        {
            //Arrange
            User? userModel = null;
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(userModel);
            var userServiceDemo = new UserServiceDemo(_mockUserRepository.Object);

            //Act
            var testResult = await userServiceDemo.LoginUser(new LoginRequest());

            //Assert
            Assert.Null(testResult);

        }

        [Theory]
        [InlineData(StaffEnum.Staff, "John Doe")]
        [InlineData(StaffEnum.Admin, "Marry Sue")]
        public async Task LoginUser_ShouldNotReturnNull(StaffEnum userType, string userName)
        {
            //Arrange
            var user = new User
            {
                Id = 1,
                Username = userName,
                Type = userType

            };

            var expectedResult = new LoginResponse
            {
                Id = 1,
                UserName = userName,
                Type = userType.ToString()
            };

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var userServiceDemo = new UserServiceDemo(_mockUserRepository.Object);

            //Act
            var testResult = await userServiceDemo.LoginUser(new LoginRequest());

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(expectedResult.Id, testResult!.Id);
            Assert.Equal(expectedResult.UserName, testResult!.UserName);
            Assert.Equal(expectedResult.Type, testResult!.Type);
        }
    }
}
