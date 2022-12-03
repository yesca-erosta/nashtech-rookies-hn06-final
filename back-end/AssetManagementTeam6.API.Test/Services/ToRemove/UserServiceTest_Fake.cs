using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
using Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Test.Services.ToRemove
{
    [ExcludeFromCodeCoverage]
    public class UserServiceTest_Fake
    {
        private IUserRepository _fakeUserRepository;
        private UserService _userService;

        public UserServiceTest_Fake()
        {
            _fakeUserRepository = new FakeUserRepository();
            _userService = new UserService(_fakeUserRepository);
        }


        //[Fact]
        public async Task CreateNewUser_ShouldNotReturnNull()
        {
            //Arrange
            var userRequest = new UserRequest
            {
                UserName = "dongnp130120001",
                FirstName = "Dong",
                LastName = "Nguyen Phuong",
                DateOfBirth = new DateTime(2000, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2010, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = false,
                Password = "Admin@123",
                Type = StaffEnum.Admin,
            };

            //Act
            var testResult = await _userService.Create(userRequest);

            //Assert
            Assert.NotNull(testResult);
            Assert.IsType<User>(testResult);
            Assert.Equal(userRequest.UserName, testResult!.UserName);
            Assert.Equal(userRequest.FirstName, testResult!.FirstName);
            Assert.Equal(userRequest.LastName, testResult!.LastName);
            Assert.Equal(userRequest.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(userRequest.Gender, testResult!.Gender);
            Assert.Equal(userRequest.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(userRequest.Location, testResult!.Location);
            Assert.True(testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(SystemFunction.CreateMD5(userRequest.Password), testResult!.Password);
            Assert.Equal(userRequest.Type, testResult!.Type);
        }
    }
}
