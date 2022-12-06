using AssetManagementTeam6.API.Dtos.Pagination;
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
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly User _sampleUser;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _sampleUser = new User
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

        public static readonly object[][] CorrectUpdateData =
        {
            new object[] { 1, new DateTime(2000, 01, 13), GenderEnum.Male, new DateTime(2022, 09, 27), StaffEnum.Admin},
            new object[] { 10, new DateTime(2000, 01, 13), GenderEnum.Male, new DateTime(2022, 09, 27), StaffEnum.Admin },
            new object[] { 100, new DateTime(2000, 01, 13), GenderEnum.Male, new DateTime(2022, 09, 27), StaffEnum.Admin },
            new object[] { 1000, new DateTime(2000, 01, 13), GenderEnum.Male, new DateTime(2022, 09, 27), StaffEnum.Admin }
        };

        public static readonly object[][] InvalidUpdateData =
        {
            // age < 18
            new object[] { 1, new DateTime(2012, 01, 13), GenderEnum.Male, new DateTime(2022, 09, 27), StaffEnum.Admin},
            // joined date > dob
            new object[] { 1, new DateTime(2000, 01, 13), GenderEnum.Male, new DateTime(1999, 09, 27), StaffEnum.Admin },
            // joindate in weekend
            new object[] { 1, new DateTime(2000, 01, 13), GenderEnum.Male, new DateTime(2022, 12, 27), StaffEnum.Admin },
        };

        public static readonly object[][] CorrectGetPagination =
        {
            //LocationEnum location, List<StaffEnum> types, string nameToQuery, string sort, int page, int pageSize
            new object[] { LocationEnum.HN, new List<StaffEnum>(), "", "", 1, 10 },
            new object[] { LocationEnum.HCM, new List<StaffEnum>(), "", "", 1, 10 },
            new object[] { LocationEnum.DN, new List<StaffEnum>(), "", "", 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() { StaffEnum.Staff}, "", "", 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() { StaffEnum.Admin}, "", "", 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() { StaffEnum.Admin, StaffEnum.Staff}, "", "", 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "dongnp", "", 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.NameAcsending, 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.NameDescending, 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.StaffCodeAcsending, 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.StaffCodeDescending, 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.JoinedDateAcsending, 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.JoinedDateDescending, 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.TypeAcsending, 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.TypeDescending, 1, 10 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.TypeDescending, 1, 2 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.TypeDescending, 2, 2 },
            new object[] { LocationEnum.HN, new List<StaffEnum>() , "", Constants.TypeDescending, 100, 10 },
        };

        public List<User> GetSampleUserLists()
        {
            return TestBase.ReadJsonFromFile<List<User>>("dummy_user_data.json"); ;
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
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(_sampleUser);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserById(1);

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(_sampleUser.Id, testResult!.Id);
            Assert.Equal(_sampleUser.UserName, testResult!.UserName);
            Assert.Equal(_sampleUser.FirstName, testResult!.FirstName);
            Assert.Equal(_sampleUser.LastName, testResult!.LastName);
            Assert.Equal(_sampleUser.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(_sampleUser.Gender, testResult!.Gender);
            Assert.Equal(_sampleUser.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(_sampleUser.Location, testResult!.Location);
            Assert.Equal(_sampleUser.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(_sampleUser.Password, testResult!.Password);
            Assert.Equal(_sampleUser.Type, testResult!.Type);
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
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(_sampleUser);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserByStaffCode("SD0001");

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(_sampleUser.Id, testResult!.Id);
            Assert.Equal(_sampleUser.UserName, testResult!.UserName);
            Assert.Equal(_sampleUser.FirstName, testResult!.FirstName);
            Assert.Equal(_sampleUser.LastName, testResult!.LastName);
            Assert.Equal(_sampleUser.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(_sampleUser.Gender, testResult!.Gender);
            Assert.Equal(_sampleUser.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(_sampleUser.Location, testResult!.Location);
            Assert.Equal(_sampleUser.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(_sampleUser.Password, testResult!.Password);
            Assert.Equal(_sampleUser.Type, testResult!.Type);
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
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(_sampleUser);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetUserByUserAccount("dongnp13");

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(_sampleUser.Id, testResult!.Id);
            Assert.Equal(_sampleUser.UserName, testResult!.UserName);
            Assert.Equal(_sampleUser.FirstName, testResult!.FirstName);
            Assert.Equal(_sampleUser.LastName, testResult!.LastName);
            Assert.Equal(_sampleUser.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(_sampleUser.Gender, testResult!.Gender);
            Assert.Equal(_sampleUser.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(_sampleUser.Location, testResult!.Location);
            Assert.Equal(_sampleUser.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(_sampleUser.Password, testResult!.Password);
            Assert.Equal(_sampleUser.Type, testResult!.Type);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnNull()
        {
            //Arrange
            User user = null!;
            _mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).ReturnsAsync(user);
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
            _mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).ReturnsAsync(_sampleUser);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.ChangePassword(_sampleUser);

            //Assert
            Assert.NotNull(testResult);
            Assert.IsType<User>(testResult);
            Assert.Equal(_sampleUser.Id, testResult!.Id);
            Assert.Equal(_sampleUser.UserName, testResult!.UserName);
            Assert.Equal(_sampleUser.FirstName, testResult!.FirstName);
            Assert.Equal(_sampleUser.LastName, testResult!.LastName);
            Assert.Equal(_sampleUser.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(_sampleUser.Gender, testResult!.Gender);
            Assert.Equal(_sampleUser.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(_sampleUser.Location, testResult!.Location);
            Assert.Equal(_sampleUser.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(_sampleUser.Password, testResult!.Password);
            Assert.Equal(_sampleUser.Type, testResult!.Type);
        }

        [Fact]
        public async Task CreateNewUser_ShouldNotReturnNull()
        {
            //Arrange
            var userRequest = new UserRequest
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

            _mockUserRepository.Setup(x => x.Create(It.IsAny<User>())).ReturnsAsync(_sampleUser);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.Create(userRequest);

            //Assert
            Assert.NotNull(testResult);
            Assert.IsType<User>(testResult);
            Assert.Equal(_sampleUser.Id, testResult!.Id);
            Assert.Equal(_sampleUser.UserName, testResult!.UserName);
            Assert.Equal(_sampleUser.FirstName, testResult!.FirstName);
            Assert.Equal(_sampleUser.LastName, testResult!.LastName);
            Assert.Equal(_sampleUser.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(_sampleUser.Gender, testResult!.Gender);
            Assert.Equal(_sampleUser.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(_sampleUser.Location, testResult!.Location);
            Assert.Equal(_sampleUser.NeedUpdatePwdOnLogin, testResult!.NeedUpdatePwdOnLogin);
            Assert.Equal(_sampleUser.Password, testResult!.Password);
            Assert.Equal(_sampleUser.Type, testResult!.Type);
        }

        [Fact]
        public async Task CreateNewUser_ShouldReturnNull()
        {
            //Arrange
            User user = null!;
            var userRequest = new UserRequest
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

            _mockUserRepository.Setup(x => x.Create(It.IsAny<User>())).ReturnsAsync(user);
            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.Create(userRequest);

            //Assert
            Assert.Null(testResult);
        }

        [Theory]
        [InlineData(LocationEnum.HN)]
        [InlineData(LocationEnum.DN)]
        [InlineData(LocationEnum.HCM)]
        public async Task GetListOfUsers_ReturnListOfUsers(LocationEnum location)
        {
            // Arrange
            var users = GetSampleUserLists();
            var userLocations = users.Where(x => x.Location == location)?.ToList() ?? new List<User>();
            var expectedType = typeof(List<GetUserResponse>);
            _mockUserRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(userLocations);
            var userService = new UserService(_mockUserRepository.Object);
            var expectedCount = userLocations.Count();

            // Act
            var testResult = await userService.GetAllAsync(location);
            var count = testResult?.Count() ?? 0;
            var type = testResult?.GetType();

            // Assert
            Assert.Equal(expectedType, type);
            Assert.Equal(expectedCount, count);
            Assert.NotNull(testResult);
        }

        [Theory]
        [InlineData(LocationEnum.HN)]
        [InlineData(LocationEnum.DN)]
        [InlineData(LocationEnum.HCM)]
        public async Task EmptyUserList_ReturnListOfNone(LocationEnum location)
        {
            // Arrange
            var users = new List<User>();
            var userLocations = users.Where(x => x.Location == location)?.ToList() ?? new List<User>();
            _mockUserRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(userLocations);
            var service = new UserService(_mockUserRepository.Object);

            // Act
            var testResult = await service.GetAllAsync(location);

            // Assert
            Assert.IsType<List<GetUserResponse>>(testResult);
            Assert.Empty(testResult);
            Assert.NotNull(testResult);
        }

        [Theory, MemberData(nameof(CorrectUpdateData))]
        public async Task UpdateUser_ShouldNotReturnNull(int id, DateTime dob, GenderEnum gender, DateTime joinedDate, StaffEnum type)
        {
            //Arrange
            var user = new User
            {
                Id = id,
                UserName = "dongnp130120001",
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = "Admin@123",
            };
            var convertUser = new UserRequest
            {
                Id = id,
                DateOfBirth = dob,
                Gender = gender,
                JoinedDate = joinedDate,
                Type = type,
                Location = LocationEnum.HN,
            };

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).ReturnsAsync(user);
            var service = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await service.Update(id, convertUser);

            //Assert
            Assert.NotNull(testResult);
            Assert.IsType<User>(testResult);
            Assert.Equal(user.Id, testResult!.Id);
            Assert.Equal(user.DateOfBirth, testResult!.DateOfBirth);
            Assert.Equal(user.Gender, testResult!.Gender);
            Assert.Equal(user.JoinedDate, testResult!.JoinedDate);
            Assert.Equal(user.Type, testResult!.Type);
            Assert.Equal(user.Location, testResult!.Location);
        }

        [Theory, MemberData(nameof(InvalidUpdateData))]
        public async Task UpdateUser_ShouldReturnNull(int id, DateTime dob, GenderEnum gender, DateTime joinedDate, StaffEnum type)
        {
            //Arrange
            User user = null!;
            var userRequest = new UserRequest
            {
                Id = id,
                DateOfBirth = dob,
                Gender = gender,
                JoinedDate = joinedDate,
                Type = type,
                Location = LocationEnum.HN,
            };

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(_sampleUser);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).ReturnsAsync(user);
            var service = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await service.Update(id, userRequest);

            //Assert
            Assert.Null(testResult);
        }

        [Fact]
        public async Task UpdateUserInvalid_CannotGetUser()
        {
            //Arrange
            User user = null!;
            var convertUser = new UserRequest
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


            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var service = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await service.Update(1, convertUser);

            //Assert
            Assert.Null(testResult);
        }

        //TODO: Handling
        [Theory, MemberData(nameof(CorrectGetPagination))]
        public async Task GetPaginationUser_ShouldReturnNotNull(LocationEnum location, List<StaffEnum> types, string nameToQuery
                                                                , string sort, int page, int pageSize)
        {
            //Arrange
            var users = GetSampleUserLists();
            var userLocations = users.Where(x => x.Location == location)?.ToList() ?? new List<User>();

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                Sort = sort,
                ValueSearch = nameToQuery?.Trim()?.ToLower() ?? string.Empty,
                Types = types,
            };

            var expectedOutput = GetExpectedPaginationUserOutput(userLocations, queryModel);
            var expectedCount = expectedOutput.Source?.Count() ?? 0;
            var expectedType = expectedOutput?.Source?.GetType();

            _mockUserRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(userLocations);

            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.GetPagination(queryModel, location);
            var count = testResult?.Source?.Count() ?? 0;
            var type = testResult?.Source?.GetType();

            Assert.NotNull(testResult);
            Assert.Equal(expectedCount, count);
            Assert.Equal(expectedType, type);
        }

        private Pagination<GetUserResponse?> GetExpectedPaginationUserOutput(List<User>? users, PaginationQueryModel queryModel)
        {
            // filter by type
            if (queryModel.Types != null)
            {
                users = users?.Where(u => queryModel.Types.Contains(u.Type))?.ToList();
            }

            // search user by staffcode or fullname
            var nameToQuery = "";
            if (!string.IsNullOrEmpty(queryModel.ValueSearch))
            {
                nameToQuery = queryModel.ValueSearch.Trim().ToLower();
                users = users?.Where(u => u!.UserName!.ToLower().Contains(nameToQuery) ||
                                        u!.StaffCode!.ToLower().Contains(nameToQuery))?.ToList();
            }

            //sorting
            var sortOption = queryModel.Sort ??= Constants.NameAcsending;

            switch (sortOption)
            {
                case Constants.NameAcsending:
                    users = users?.OrderBy(u => u.FullName)?.ToList();
                    break;
                case Constants.NameDescending:
                    users = users?.OrderByDescending(u => u.FullName)?.ToList();
                    break;
                case Constants.StaffCodeAcsending:
                    users = users?.OrderBy(u => u.StaffCode)?.ToList();
                    break;
                case Constants.StaffCodeDescending:
                    users = users?.OrderByDescending(u => u.StaffCode)?.ToList();
                    break;
                case Constants.JoinedDateAcsending:
                    users = users?.OrderBy(u => u.JoinedDate)?.ToList();
                    break;
                case Constants.JoinedDateDescending:
                    users = users?.OrderByDescending(u => u.JoinedDate)?.ToList();
                    break;
                case Constants.TypeAcsending:
                    users = users?.OrderBy(u => u.Type)?.ToList();
                    break;
                case Constants.TypeDescending:
                    users = users?.OrderByDescending(u => u.Type)?.ToList();
                    break;
                default:
                    users = users?.OrderBy(u => u.FullName)?.ToList();
                    break;
            }

            //paging
            if (users == null || !users.Any())
            {
                return new Pagination<GetUserResponse?>
                {
                    Source = null,
                    TotalPage = 1,
                    TotalRecord = 0,
                    QueryModel = queryModel
                };
            }

            var output = new Pagination<GetUserResponse>();

            output.TotalRecord = users.Count();

            var listUsers = users.Select(user => new GetUserResponse(user));

            output.Source = listUsers.Skip((queryModel.Page - 1) * queryModel.PageSize)
                                    .Take(queryModel.PageSize)
                                    .ToList();

            output.TotalPage = (output.TotalRecord - 1) / queryModel.PageSize + 1;

            if (queryModel.Page > output.TotalPage)
            {
                queryModel.Page = output.TotalPage;
            }

            output.QueryModel = queryModel;

            return output!;
        }

        [Fact]
        public async Task DeleteUserSuccess_ShouldReturnTrue()
        {
            //Arrange
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(_sampleUser);

            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.Delete(1);

            //Assert
            Assert.True(testResult);
        }

        [Fact]
        public async Task DeleteUserFail_ShouldReturnFalse()
        {
            //Arrange
            User user = null!;

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);

            var userService = new UserService(_mockUserRepository.Object);

            //Act
            var testResult = await userService.Delete(1);

            //Assert
            Assert.False(testResult);
        }
    }
}
