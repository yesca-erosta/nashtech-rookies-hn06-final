using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.ErrorHandling;
using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
using Common.Enums;
using Moq;
using System.Linq.Expressions;

namespace AssetManagementTeam6.API.Test.Services
{
    public class RequestForReturningTest
    {
        private Mock<IRequestForReturningRepository> _mockRequestForReturningRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IAssignmentRepository> _mockAssignmentRepository;
        private RequestForReturning _sample;
        private static Assignment _assignmentSample = new Assignment
        {
            AssignedDate = DateTime.Now,
            Note = "Assignment 1",
            IsReturning = false,
            State = AssignmentStateEnum.WaitingForAcceptance,
            Id = 1,
            AssetId = 1,
            AssignedToId = 2,
            AssignedById = 1,
            Asset = new Asset
            {
                Id = 1,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",

                Category = new Category
                {
                    Id = "LA",
                    Name = "Laptop"
                }
            },
            AssignedBy = new User
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
            },
            AssignedTo = new User
            {
                Id = 2,
                UserName = "tineship",
                FirstName = "Tien",
                LastName = "Nguyen",
                DateOfBirth = new DateTime(1999, 01, 13),
                Gender = GenderEnum.Male,
                JoinedDate = new DateTime(2012, 10, 10),
                Location = LocationEnum.HN,
                NeedUpdatePwdOnLogin = true,
                Password = SystemFunction.CreateMD5("Admin@123"),
                Type = StaffEnum.Staff,
            }
        };
        private static Asset _assetSample = new Asset
        {
            Id = 1,
            AssetName = "Laptop Sample",
            InstalledDate = new DateTime(2000, 01, 13),
            State = AssetStateEnum.NotAvailable,
            Location = LocationEnum.HN,
            Specification = "null",
            CategoryId = "LA",

            Category = new Category
            {
                Id = "LA",
                Name = "Laptop"
            }
        };
        public List<RequestForReturning> GetListRfrSample ()
        {
            return new List<RequestForReturning>
            {
                new RequestForReturning()
            {
                ReturnedDate = DateTime.Now,
                State = RequestForReturningStateEnum.WaitingForReturning,
                Id = 1,
                AssignmentId = 1,
                RequestedById = 1,
                AcceptedById = 2,
                Assignment = new Assignment
                {
                    AssignedDate = DateTime.Now,
                    Note = "Assignment 1",
                    IsReturning = false,
                    State = AssignmentStateEnum.WaitingForAcceptance,
                    Id = 1,
                    AssetId = 1,
                    AssignedToId = 2,
                    AssignedById = 1,
                    Asset = new Asset
                    {
                        Id = 1,
                        AssetName = "Laptop Sample",
                        InstalledDate = new DateTime(2000, 01, 13),
                        State = AssetStateEnum.NotAvailable,
                        Location = LocationEnum.HN,
                        Specification = "null",
                        CategoryId = "LA",
                        AssetCode = "LA00001",

                        Category = new Category
                        {
                            Id = "LA",
                            Name = "Laptop"
                        }
                    },
                    AssignedBy = new User
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
                    },
                    AssignedTo = new User
                    {
                        Id = 2,
                        UserName = "tineship",
                        FirstName = "Tien",
                        LastName = "Nguyen",
                        DateOfBirth = new DateTime(1999, 01, 13),
                        Gender = GenderEnum.Male,
                        JoinedDate = new DateTime(2012, 10, 10),
                        Location = LocationEnum.HN,
                        NeedUpdatePwdOnLogin = true,
                        Password = SystemFunction.CreateMD5("Admin@123"),
                        Type = StaffEnum.Staff,
                    }
                },
                RequestedBy = new User
                {
                    Id = 2,
                    UserName = "tineship",
                    FirstName = "Tien",
                    LastName = "Nguyen",
                    DateOfBirth = new DateTime(1999, 01, 13),
                    Gender = GenderEnum.Male,
                    JoinedDate = new DateTime(2012, 10, 10),
                    Location = LocationEnum.HN,
                    NeedUpdatePwdOnLogin = true,
                    Password = SystemFunction.CreateMD5("Admin@123"),
                    Type = StaffEnum.Staff,
                },
                AcceptedBy = new User
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
                },
            }
,
                new RequestForReturning()
            {
                ReturnedDate = DateTime.Now,
                State = RequestForReturningStateEnum.WaitingForReturning,
                Id = 1,
                AssignmentId = 1,
                RequestedById = 1,
                AcceptedById = 2,
                Assignment = new Assignment
                {
                    AssignedDate = DateTime.Now,
                    Note = "Assignment 1",
                    IsReturning = false,
                    State = AssignmentStateEnum.WaitingForAcceptance,
                    Id = 1,
                    AssetId = 1,
                    AssignedToId = 2,
                    AssignedById = 1,
                    Asset = new Asset
                    {
                        Id = 1,
                        AssetName = "Laptop Sample",
                        InstalledDate = new DateTime(2000, 01, 13),
                        State = AssetStateEnum.NotAvailable,
                        Location = LocationEnum.HN,
                        Specification = "null",
                        CategoryId = "LA",
                        AssetCode = "LA00002",

                        Category = new Category
                        {
                            Id = "LA",
                            Name = "Laptop"
                        }
                    },
                    AssignedBy = new User
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
                    },
                    AssignedTo = new User
                    {
                        Id = 2,
                        UserName = "tineship",
                        FirstName = "Tien",
                        LastName = "Nguyen",
                        DateOfBirth = new DateTime(1999, 01, 13),
                        Gender = GenderEnum.Male,
                        JoinedDate = new DateTime(2012, 10, 10),
                        Location = LocationEnum.HN,
                        NeedUpdatePwdOnLogin = true,
                        Password = SystemFunction.CreateMD5("Admin@123"),
                        Type = StaffEnum.Staff,
                    }
                },
                RequestedBy = new User
                {
                    Id = 2,
                    UserName = "tineship",
                    FirstName = "Tien",
                    LastName = "Nguyen",
                    DateOfBirth = new DateTime(1999, 01, 13),
                    Gender = GenderEnum.Male,
                    JoinedDate = new DateTime(2012, 10, 10),
                    Location = LocationEnum.HN,
                    NeedUpdatePwdOnLogin = true,
                    Password = SystemFunction.CreateMD5("Admin@123"),
                    Type = StaffEnum.Staff,
                },
                AcceptedBy = new User
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
                },
            }

        };
        }

        public RequestForReturningTest()
        {
            _mockRequestForReturningRepository = new Mock<IRequestForReturningRepository>();
            _mockAssignmentRepository = new Mock<IAssignmentRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _sample = new RequestForReturning()
            {
                ReturnedDate = DateTime.Now,
                State = RequestForReturningStateEnum.WaitingForReturning,
                Id = 1,
                AssignmentId = 1,
                RequestedById = 1,
                AcceptedById = 2,
                Assignment = new Assignment
                {
                    AssignedDate = DateTime.Now,
                    Note = "Assignment 1",
                    IsReturning = false,
                    State = AssignmentStateEnum.WaitingForAcceptance,
                    Id = 1,
                    AssetId = 1,
                    AssignedToId = 2,
                    AssignedById = 1,
                    Asset = new Asset
                    {
                        Id = 1,
                        AssetName = "Laptop Sample",
                        InstalledDate = new DateTime(2000, 01, 13),
                        State = AssetStateEnum.NotAvailable,
                        Location = LocationEnum.HN,
                        Specification = "null",
                        CategoryId = "LA",

                        Category = new Category
                        {
                            Id = "LA",
                            Name = "Laptop"
                        }
                    },
                    AssignedBy = new User
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
                    },
                    AssignedTo = new User
                    {
                        Id = 2,
                        UserName = "tineship",
                        FirstName = "Tien",
                        LastName = "Nguyen",
                        DateOfBirth = new DateTime(1999, 01, 13),
                        Gender = GenderEnum.Male,
                        JoinedDate = new DateTime(2012, 10, 10),
                        Location = LocationEnum.HN,
                        NeedUpdatePwdOnLogin = true,
                        Password = SystemFunction.CreateMD5("Admin@123"),
                        Type = StaffEnum.Staff,
                    }
                },
                RequestedBy = new User
                {
                    Id = 2,
                    UserName = "tineship",
                    FirstName = "Tien",
                    LastName = "Nguyen",
                    DateOfBirth = new DateTime(1999, 01, 13),
                    Gender = GenderEnum.Male,
                    JoinedDate = new DateTime(2012, 10, 10),
                    Location = LocationEnum.HN,
                    NeedUpdatePwdOnLogin = true,
                    Password = SystemFunction.CreateMD5("Admin@123"),
                    Type = StaffEnum.Staff,
                },
                AcceptedBy = new User
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
                },    
            };
        }

        [Fact]
        public void GetFilterByReturnedDate_ShouldReturnFalse()
        {
            //Arrange
            var rfr = new RequestForReturning();
            var queryModel = new PaginationQueryModel();

            queryModel.FilterByReturnedDates = new DateTime(2001,1,13);

            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
                _mockAssignmentRepository.Object,_mockUserRepository.Object);

            //Act
            var result = requestForReturnService.GetFilterByReturnedDate(rfr,queryModel);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetFilterByReturnedDate_ShouldReturnTrue()
        {
            //Arrange
            var rfr = new RequestForReturning();
            var queryModel = new PaginationQueryModel();
            rfr.ReturnedDate = new DateTime(2001, 1, 13);
            queryModel.FilterByReturnedDates = new DateTime(2001, 1, 13);

            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
                _mockAssignmentRepository.Object, _mockUserRepository.Object);

            //Act
            var result = requestForReturnService.GetFilterByReturnedDate(rfr, queryModel);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetRequestForReturningById_ShouldReturnNull()
        {
            //Arrange
            RequestForReturning? rfr = null;

            _mockRequestForReturningRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<RequestForReturning, bool>>>())).ReturnsAsync(rfr);
            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
                _mockAssignmentRepository.Object, _mockUserRepository.Object);
            //Act
            var result = await requestForReturnService.GetRequestForReturningById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetRequestForReturningById_ShouldReturnNotNull()
        {
            //Arrange
            
            _mockRequestForReturningRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<RequestForReturning, bool>>>())).ReturnsAsync(_sample);
            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
                 _mockAssignmentRepository.Object, _mockUserRepository.Object);

            //Act
            var result = await requestForReturnService.GetRequestForReturningById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.State,_sample.State);
            Assert.Equal(result.Id,_sample.Id);
            Assert.Equal(result.AcceptedById, _sample.AcceptedById);
            Assert.Equal(result.AssignmentId, _sample.AssignmentId);
            Assert.Equal(result.RequestedById, _sample.RequestedById);
            Assert.Equal(result.ReturnedDate, _sample.ReturnedDate);
        }
        
        public static readonly object[][] FalseThrowException =
       {
            new object[] { RequestForReturningStateEnum.Completed,new Assignment(),AssignmentStateEnum.Accepted,new Asset(),AssetStateEnum.Assigned,new User() },
            new object[] { RequestForReturningStateEnum.Completed,_assignmentSample,AssignmentStateEnum.Deleted,new Asset(),AssetStateEnum.Assigned,new User() },
            new object[] { RequestForReturningStateEnum.Completed,_assignmentSample, AssignmentStateEnum.Deleted,_assetSample,AssetStateEnum.Assigned,new User() },
            new object[] { RequestForReturningStateEnum.WaitingForReturning,null!,AssignmentStateEnum.Accepted,new Asset(),AssetStateEnum.Assigned,new User() },
            new object[] { RequestForReturningStateEnum.Completed,new Assignment(),AssignmentStateEnum.Accepted,new Asset(),AssetStateEnum.Assigned,null! },
            new object[] { RequestForReturningStateEnum.Completed,new Assignment(),AssignmentStateEnum.Accepted,new Asset(),AssetStateEnum.Assigned,null! },
        };

        [Theory, MemberData(nameof(FalseThrowException))]
        public async Task CancelReturningRequest_ThrowException(RequestForReturningStateEnum rfrState,Assignment assignment, AssignmentStateEnum assignmentState,Asset asset,AssetStateEnum assetState,User requestedBy )
        {
            //Arrange
            var failedSample = new RequestForReturning
            {
                State = rfrState,
                RequestedBy = requestedBy,
                Assignment = assignment,
                Id = 1,
                ReturnedDate = DateTime.Now,
                RequestedById = 1,
                AcceptedBy = _sample.AcceptedBy,
            };

            //failedSample.Assignment.Asset = asset;
            //failedSample.Assignment.Asset.State = assetState;
            //failedSample.Assignment.State = assignmentState;

            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
               _mockAssignmentRepository.Object, _mockUserRepository.Object);

            //Act
            //var result = await requestForReturnService.CancelReturningRequest(failedSample);
            Func<Task> act = () => requestForReturnService.CancelReturningRequest(failedSample);

            //Assert
            //Assert.NotNull(result);
            //Assert.IsType<MyCustomException>(result);
            var exception = await Assert.ThrowsAsync<MyCustomException>(act);
        }

        [Fact]
        public async Task CancelReturningRequest_ReturnNotNull()
        {
            //Arrange
            _mockRequestForReturningRepository.Setup(x => x.Delete(It.IsAny<RequestForReturning>())).ReturnsAsync(true);
            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
               _mockAssignmentRepository.Object, _mockUserRepository.Object);
            _sample.Assignment.State = AssignmentStateEnum.Accepted;
            _sample.Assignment.Asset.State = AssetStateEnum.Assigned;
            //Act
            var result = await requestForReturnService.CancelReturningRequest(_sample);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.State, _sample.State);
            Assert.Equal(result.Id, _sample.Id);
            Assert.Equal(result.AcceptedBy, _sample.AcceptedBy);
            Assert.Equal(result.RequestedBy, _sample.RequestedBy);
            Assert.Equal(result.ReturnedDate, _sample.ReturnedDate);
        }

        [Theory, MemberData(nameof(FalseThrowException))]
        public async Task CompleteReturningRequest_ThrowException(RequestForReturningStateEnum rfrState, Assignment assignment, AssignmentStateEnum assignmentState, Asset asset, AssetStateEnum assetState, User requestedBy)
        {
            //Arrange
            var failedSample = new RequestForReturning
            {
                State = rfrState,
                RequestedBy = requestedBy,
                Assignment = assignment,
                Id = 1,
                ReturnedDate = DateTime.Now,
                RequestedById = 1,
                AcceptedBy = _sample.AcceptedBy,
            };

            //failedSample.Assignment.Asset = asset;
            //failedSample.Assignment.Asset.State = assetState;
            //failedSample.Assignment.State = assignmentState;

            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
               _mockAssignmentRepository.Object, _mockUserRepository.Object);

            //Act
            //var result = await requestForReturnService.CancelReturningRequest(failedSample);
            Func<Task> act = () => requestForReturnService.CompleteReturningRequest(failedSample);

            //Assert
            //Assert.NotNull(result);
            //Assert.IsType<MyCustomException>(result);
            var exception = await Assert.ThrowsAsync<MyCustomException>(act);
        }

        [Fact]
        public async Task CompleteReturningRequest_ReturnNotNull()
        {
            //Arrange
            _mockAssignmentRepository.Setup(x => x.Update(It.IsAny<Assignment>())).ReturnsAsync(_sample.Assignment);
            _mockRequestForReturningRepository.Setup(x => x.Update(It.IsAny<RequestForReturning>())).ReturnsAsync(_sample);
            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
               _mockAssignmentRepository.Object, _mockUserRepository.Object);
            _sample.Assignment.State = AssignmentStateEnum.Accepted;
            _sample.Assignment.Asset.State = AssetStateEnum.Assigned;
            //Act
            var result = await requestForReturnService.CompleteReturningRequest(_sample);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.State, _sample.State);
            Assert.Equal(result.Id, _sample.Id);
            Assert.Equal(result.AcceptedBy, _sample.AcceptedBy);
            Assert.Equal(result.RequestedBy, _sample.RequestedBy);
            Assert.Equal(result.ReturnedDate, _sample.ReturnedDate);
        }

        [Theory, MemberData(nameof(FalseThrowException))]
        public async Task Create_ThrowException(RequestForReturningStateEnum rfrState, Assignment assignment, AssignmentStateEnum assignmentState, Asset asset, AssetStateEnum assetState, User requestedBy)
        {
            //Arrange
            var failedSample = new RequestForReturning
            {
                State = rfrState,
                RequestedBy = requestedBy,
                Assignment = assignment,
                Id = 1,
                ReturnedDate = DateTime.Now,
                RequestedById = 1,
                AcceptedBy = _sample.AcceptedBy,
            };

            var request = new RequestForReturningRequest
            {
            };

            _mockAssignmentRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Assignment, bool>>>())).ReturnsAsync(assignment);
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(requestedBy);

            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
               _mockAssignmentRepository.Object, _mockUserRepository.Object);

            //Act
            //var result = await requestForReturnService.CancelReturningRequest(failedSample);
            Func<Task> act = () => requestForReturnService.Create(request);

            //Assert
            //Assert.NotNull(result);
            //Assert.IsType<MyCustomException>(result);
            var exception = await Assert.ThrowsAsync<MyCustomException>(act);
        }

        [Fact]
        public async Task Create_ReturnNotNull()
        {
            //Arrange
            var request = new RequestForReturningRequest
            {
            };

            _mockAssignmentRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Assignment, bool>>>())).ReturnsAsync(_sample.Assignment);
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(_sample.RequestedBy);

            _mockRequestForReturningRepository.Setup(x => x.Create(It.IsAny<RequestForReturning>())).ReturnsAsync(_sample);
            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
               _mockAssignmentRepository.Object, _mockUserRepository.Object);
            _sample.Assignment.State = AssignmentStateEnum.Accepted;
            _sample.Assignment.Asset.State = AssetStateEnum.Assigned;
            //Act
            var result = await requestForReturnService.Create(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.State, _sample.State);
            Assert.Equal(result.Id, _sample.Id);
            Assert.Equal(result.AcceptedBy, _sample.AcceptedBy);
            Assert.Equal(result.RequestedBy, _sample.RequestedBy);
            Assert.Equal(result.ReturnedDate, _sample.ReturnedDate);
        }

        public static readonly object[][] CorrectGetPagination =
{
            //List types, string nameToQuery, string sort, int page, int pageSize, DateTime date
            new object[] { new List<RequestForReturningStateEnum>(), "", Constants.RequestForReturningIdAcsending, 1, 10, DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>(), "a", Constants.RequestForReturningIdAcsending, 1, 10, DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>(), "", Constants.RequestForReturningIdDescending, 1, 10, DateTime.UtcNow},
            new object[] { new List<RequestForReturningStateEnum>(), "", Constants.RequestForReturningCodeAcsending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() { RequestForReturningStateEnum.Completed}, "", Constants.RequestForReturningCodeDescending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() { RequestForReturningStateEnum.WaitingForReturning }, "", Constants.RequestForReturningNameAcsending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() { RequestForReturningStateEnum.Completed, RequestForReturningStateEnum.WaitingForReturning}, "", "", 1, 10, DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningNameDescending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningRequestedByAcsending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningRequestedByDescending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningAssignedDateAcsending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningAssignedDateDescending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningAcceptedByAcsending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningAcceptedByDescending, 1, 10 , DateTime.UtcNow},
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningReturnedDateAcsending, 1, 10 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningReturnedDateDescending, 1, 2 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningStateAcsending, 1, 2 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", Constants.RequestForReturningStateDescending, 1, 2 , DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>() , "", "", 2, 2 ,DateTime.UtcNow},
            new object[] { new List<RequestForReturningStateEnum>() , "", "", 100, 10,DateTime.UtcNow },
            new object[] { new List<RequestForReturningStateEnum>(), "", "", 1, 10 , new DateTime(2000,01, 13)},
        };

        [Theory, MemberData(nameof(CorrectGetPagination))]
        public async Task GetPaginationUser_ShouldReturnNotNull(List<RequestForReturningStateEnum> states, string nameToQuery
                                                                , string sort, int page, int pageSize, DateTime? date)
        {
            //Arrange
            var rfrList = GetListRfrSample();

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                Sort = sort,
                ValueSearch = nameToQuery?.Trim().ToLower() ?? string.Empty,
                RequestForReturningState = states.Count != 0 ? states : null,
                FilterByReturnedDates = date
            };

            var expectedOutput = GetExpectedPaginationRfrOutput(rfrList, queryModel);
            var expectedCount = expectedOutput.Source?.Count() ?? 0;
            var expectedType = expectedOutput?.Source?.GetType();
            queryModel.Page = page;

            _mockRequestForReturningRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<RequestForReturning, bool>>>())).ReturnsAsync(rfrList);

            var requestForReturnService = new RequestForReturningService(_mockRequestForReturningRepository.Object,
               _mockAssignmentRepository.Object, _mockUserRepository.Object);

            //Act
            var testResult = await requestForReturnService.GetPagination(queryModel);
            var count = testResult?.Source?.Count() ?? 0;
            var type = testResult?.Source?.GetType();

            Assert.NotNull(testResult);
            Assert.Equal(expectedCount, count);
            Assert.Equal(expectedType, type);
        }
        public bool GetFilterByReturnedDate(RequestForReturning r, PaginationQueryModel queryModel)
        {
            if (r.ReturnedDate.HasValue)
            {
                var resultDay = r.ReturnedDate.Value.Day;
                var resultMonth = r.ReturnedDate.Value.Month;
                var resultYear = r.ReturnedDate.Value.Year;

                return resultDay.Equals(queryModel.FilterByReturnedDates.Value.Day)
                    && resultMonth.Equals(queryModel.FilterByReturnedDates.Value.Month)
                    && resultYear.Equals(queryModel.FilterByReturnedDates.Value.Year);
            }
            return false;
        }
        public Pagination<GetRequestForReturningResponse?> GetExpectedPaginationRfrOutput(List<RequestForReturning>? requests, PaginationQueryModel queryModel)
        {
            // search assignment by assetcode or assetname or assignee
            var nameToQuery = "";
            if (!string.IsNullOrEmpty(queryModel.ValueSearch))
            {
                nameToQuery = queryModel.ValueSearch.Trim().ToLower();

                requests = requests?.Where(u => u!.Assignment.Asset.AssetCode!.ToLower().Contains(nameToQuery) ||
                                        u!.Assignment.Asset.AssetName!.ToLower().Contains(nameToQuery) ||
                                        u!.RequestedBy.UserName!.ToLower().Contains(nameToQuery)).ToList();
            }

            if (queryModel.RequestForReturningState != null)
            {
                requests = requests?.Where(r => queryModel.RequestForReturningState.Contains(r.State))?.ToList();
            }
            if (queryModel.FilterByReturnedDates != null)
            {
                requests = requests?.Where(r => GetFilterByReturnedDate(r, queryModel)).ToList();
            }

            // sorting
            var sortOption = queryModel.Sort ??= Constants.RequestForReturningIdAcsending;

            switch (sortOption)
            {
                case Constants.RequestForReturningIdAcsending:
                    requests = requests?.OrderBy(r => r.Id)?.ToList();
                    break;
                case Constants.RequestForReturningIdDescending:
                    requests = requests?.OrderByDescending(r => r.Id)?.ToList();
                    break;
                case Constants.RequestForReturningCodeAcsending:
                    requests = requests?.OrderBy(r => r.Assignment.Asset.AssetCode)?.ToList();
                    break;
                case Constants.RequestForReturningCodeDescending:
                    requests = requests?.OrderByDescending(r => r.Assignment.Asset.AssetCode)?.ToList();
                    break;
                case Constants.RequestForReturningNameAcsending:
                    requests = requests?.OrderBy(r => r.Assignment.Asset.AssetName)?.ToList();
                    break;
                case Constants.RequestForReturningNameDescending:
                    requests = requests?.OrderByDescending(r => r.Assignment.Asset.AssetName)?.ToList();
                    break;
                case Constants.RequestForReturningRequestedByAcsending:
                    requests = requests?.OrderBy(r => r.RequestedBy.UserName)?.ToList();
                    break;
                case Constants.RequestForReturningRequestedByDescending:
                    requests = requests?.OrderByDescending(r => r.RequestedBy.UserName)?.ToList();
                    break;
                case Constants.RequestForReturningAcceptedByAcsending:
                    requests = requests?.OrderBy(r => r.AcceptedBy?.UserName)?.ToList();
                    break;
                case Constants.RequestForReturningAcceptedByDescending:
                    requests = requests?.OrderByDescending(r => r.AcceptedBy?.UserName)?.ToList();
                    break;
                case Constants.RequestForReturningAssignedDateAcsending:
                    requests = requests?.OrderBy(r => r.Assignment.AssignedDate)?.ToList();
                    break;
                case Constants.RequestForReturningAssignedDateDescending:
                    requests = requests?.OrderByDescending(r => r.Assignment.AssignedDate)?.ToList();
                    break;
                case Constants.RequestForReturningStateAcsending:
                    requests = requests?.OrderBy(r => r.State)?.ToList();
                    break;
                case Constants.RequestForReturningStateDescending:
                    requests = requests?.OrderByDescending(r => r.State)?.ToList();
                    break;
                case Constants.RequestForReturningReturnedDateAcsending:
                    requests = requests?.OrderBy(r => r.ReturnedDate)?.ToList();
                    break;
                case Constants.RequestForReturningReturnedDateDescending:
                    requests = requests?.OrderByDescending(r => r.ReturnedDate)?.ToList();
                    break;
                default:
                    requests = requests?.OrderBy(r => r.Id)?.ToList();
                    break;
            }

            //paging
            var output = new Pagination<GetRequestForReturningResponse>();

            output.TotalRecord = requests.Count();

            var listRequests = requests.Select(ass => new GetRequestForReturningResponse(ass));

            output.Source = listRequests.Skip((queryModel.Page - 1) * queryModel.PageSize)
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


    }
}
