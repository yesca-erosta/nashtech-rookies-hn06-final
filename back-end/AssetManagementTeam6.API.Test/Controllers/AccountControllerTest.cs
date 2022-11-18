using AssetManagementTeam6.API.Controllers;
using AssetManagementTeam6.API.Dtos.Models;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementTeam6.API.Test.Controllers
{
    public class AccountControllerTest
    {
        private Mock<IUserService> _mockUserService;

        public AccountControllerTest ()
        {
            _mockUserService = new Mock<IUserService>();
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest()
        {
            //Arrange
            var expectedMessage = "Username or password is incorrect!";

            LoginResponse? loginResponse = null;
            _mockUserService.Setup(x => x.LoginUser(It.IsAny<LoginRequest>())).ReturnsAsync(loginResponse);

            var accountController = new AccountController(_mockUserService.Object);

            //Act
            var result = await accountController.Login(new LoginRequest());
            var contentResult = result as BadRequestObjectResult;
            var actualMessage = contentResult?.Value?.ToString() ?? string.Empty;

            //Assert
            Assert.NotNull(contentResult);
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Theory]
        [InlineData(StaffEnum.Staff, "John Doe")]
        [InlineData(StaffEnum.Admin, "Marry Sue")]
        public async Task Login_ShouldReturnTocken(StaffEnum userType, string userName)
        {
            //Arrange
            var loginResponse = new LoginResponse
            {
                Id = 1,
                UserName = userName,
                Type = userType.ToString()
            };

            _mockUserService.Setup(x => x.LoginUser(It.IsAny<LoginRequest>())).ReturnsAsync(loginResponse);

            var accountController = new AccountController(_mockUserService.Object);

            //Act
            var result = await accountController.Login(new LoginRequest());
            var contentResult = result as OkObjectResult;
            var actualResponse = contentResult?.Value as LoginResponse;
            //Assert
            Assert.NotNull(contentResult);
            Assert.Equal(userName, actualResponse!.UserName);
            Assert.True(actualResponse!.Type.Equals(userType.ToString()));
            Assert.NotNull(actualResponse!.Token);
            Assert.NotEmpty(actualResponse!.Token);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        [InlineData(12391)]
        public async Task GetUserById_ShouldThrowException(int userId)
        {
            //Arrange
            var expectedMessage = "Bad request";
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).Throws<Exception>();
            var accountController = new AccountController(_mockUserService.Object);

            //Act
            var result = await accountController.GetUserById(userId);
            var contentResult = result as BadRequestObjectResult;
            var actualMessage = contentResult?.Value?.ToString();
            //Assert
            Assert.NotNull(contentResult);
            Assert.Equal(expectedMessage, actualMessage);
        }


        [Theory]
        [InlineData(1, "John Doe")]
        [InlineData(2, "Marry Sue")]
        [InlineData(999, "John Doe")]
        [InlineData(12391, "Marry Sue")]
        public async Task GetUserById_ShouldReturnOK(int id, string userName)
        {
            //Arrange
            var userModel = new UserModel
            {
                Id = id,
                UserName = userName,
            };
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(userModel);
            var accountController = new AccountController(_mockUserService.Object);

            //Act
            var result = await accountController.GetUserById(id);
            var contentResult = result as OkObjectResult;
            var actualResponse = contentResult?.Value as UserModel;
            //Assert
            Assert.NotNull(contentResult);
            Assert.Equal(userName, actualResponse!.UserName);
            Assert.Equal(id, actualResponse!.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-999)]
        [InlineData(-12391)]
        public async Task GetUserById_ShouldReturnBadRequest(int id)
        {
            //Arrange
            var accountController = new AccountController(_mockUserService.Object);

            //Act
            var result = await accountController.GetUserById(id);
            var contentResult = result as BadRequestResult;

            //Assert
            Assert.NotNull(contentResult);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        [InlineData(12391)]
        public async Task GetUserById_ShouldReturnNotFound(int id)
        {
            //Arrange
            UserModel? userModel = null;
            _mockUserService.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(userModel);
            var accountController = new AccountController(_mockUserService.Object);

            //Act
            var result = await accountController.GetUserById(id);
            var contentResult = result as NotFoundResult;

            //Assert
            Assert.NotNull(contentResult);
        }

        //[Fact]
        //public void CheckLogin_ShouldReturnBadRequest()
        //{
        //    //Arrange
        //    var accountController = new AccountController(_mockUserService.Object);
        //    //Act
        //    var result = accountController.CheckLogin();
        //    var contentResult = result as BadRequestResult;
        //    //Assert
        //    Assert.NotNull(contentResult);
        //}

        //[Fact]
        //public async Task CheckLogin_ShouldReturnOK()
        //{
        //    //Arrange
        //    var loginResponse = new LoginResponse
        //    {
        //        Id = 1,
        //        UserName = "abc",
        //        Type = StaffEnum.Staff.ToString()
        //    };

        //    _mockUserService.Setup(x => x.LoginUser(It.IsAny<LoginRequest>())).ReturnsAsync(loginResponse);

        //    var accountController = new AccountController(_mockUserService.Object)
        //    {
        //        ControllerContext = new ControllerContext()
        //    };
        //    var loginResult = await accountController.Login(new LoginRequest());
        //    var token = loginResponse!.Token;

        //    accountController.ControllerContext.HttpContext = new DefaultHttpContext();
        //    accountController.ControllerContext.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";
        //    //Act

        //    var result = accountController.CheckLogin();
        //    var contentResult = result as OkObjectResult;
        //    var actualId = contentResult?.Value as int?;
        //    //Assert
        //    Assert.NotNull(contentResult);
        //    Assert.Equal(1, actualId);
        //}
    }
}
