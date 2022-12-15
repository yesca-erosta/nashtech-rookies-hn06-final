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
    public class AssignmentServiceTest
    {
        private Mock<IAssignmentRepository> _mockAssignmentRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IAssetRepository> _mockAssetRepository;
        private readonly Assignment _sampleAssignment;
        private readonly Asset _sampleAsset;
        private readonly User _sampleAssignedTo;
        private readonly User _sampleAssignedBy;
        private readonly Category _sampleCategory;

        public AssignmentServiceTest()
        {
            _mockAssignmentRepository = new Mock<IAssignmentRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockAssetRepository = new Mock<IAssetRepository>();
            _sampleAssignment = new Assignment
            {
                AssetId = 1,
                AssignedToId = 2,
                AssignedById = 1,
                AssignedDate = DateTime.UtcNow,
                State = AssignmentStateEnum.WaitingForAcceptance,
                Note = "Unit Test"
            };
            _sampleCategory = new Category
            {
                Id = "LA",
                Name = "Laptop"
            };
            _sampleAsset = new Asset
            {
                Id = 1,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.Available,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                Category = _sampleCategory
            };
            _sampleAssignedTo = new User
            {
                Id = 2
            };
            _sampleAssignedBy = new User
            {
                Id = 1
            };
        }

        private static Asset _assetSampleFailed = new Asset
        {
            Id = 1,
            AssetName = "Laptop Sample",
            InstalledDate = new DateTime(2000, 01, 13),
            State = AssetStateEnum.Assigned,
            Location = LocationEnum.HN,
            Specification = "null",
            CategoryId = "LA",

            Category = new Category
            {
                Id = "LA",
                Name = "Laptop"
            }
        };
        private static Asset _asset = new Asset
        {
            Id = 1,
            AssetName = "Laptop Sample",
            InstalledDate = new DateTime(2000, 01, 13),
            State = AssetStateEnum.Available,
            Location = LocationEnum.HN,
            Specification = "null",
            CategoryId = "LA",

            Category = new Category
            {
                Id = "LA",
                Name = "Laptop"
            }
        };

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

        public static readonly object[][] CorrectGetPagination =
{
            //LocationEnum location, List<StaffEnum> types, string nameToQuery, string sort, int page, int pageSize
            new object[] { new List<AssignmentStateEnum>(), "", "", 1, 10 },
            new object[] { new List<AssignmentStateEnum>(), "", "", 1, 10 },
            new object[] { new List<AssignmentStateEnum>(), "", "", 1, 10 },
            new object[] { new List<AssignmentStateEnum>() { AssignmentStateEnum.Accepted}, "", "", 1, 10 },
            new object[] { new List<AssignmentStateEnum>() { AssignmentStateEnum.WaitingForAcceptance}, "", "", 1, 10 },
            new object[] { new List<AssignmentStateEnum>() { AssignmentStateEnum.Accepted, AssignmentStateEnum.WaitingForAcceptance,AssignmentStateEnum.Declined}, "", "", 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "dongnp", "", 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.NameAcsending, 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.NameDescending, 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.StaffCodeAcsending, 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.StaffCodeDescending, 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.JoinedDateAcsending, 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.JoinedDateDescending, 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.TypeAcsending, 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.TypeDescending, 1, 10 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.TypeDescending, 1, 2 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.TypeDescending, 2, 2 },
            new object[] { new List<AssignmentStateEnum>() , "", Constants.TypeDescending, 100, 10 },
        };

        public static readonly object[][] FalseThrowException =
        {
            //Asset asset, User assignedTo, User assignedBy, DateTime assignedDate, List<Assignment> assignments
            new object[] { null!, new User(), new User(), DateTime.UtcNow, new List<Assignment>()},
            new object[] { _assetSampleFailed, new User(), new User(), DateTime.UtcNow, new List<Assignment>() },
            new object[] { _asset, null!, new User(), DateTime.UtcNow, new List<Assignment>() },
            new object[] { _asset, new User(), new User(), DateTime.UtcNow, new List<Assignment> { _assignmentSample} },
        };


        public List<Assignment> GetSampleAssignmentLists()
        {
            return TestBase.ReadJsonFromFile<List<Assignment>>("dummy_assignment_data.json");
        }

        [Fact]
        public async Task CreateNewAssignment_ShouldNotReturnNull()
        {
            //Arrange
            var assignment = _sampleAssignment;
            var asset = _sampleAsset;
            var assignedTo = _sampleAssignedTo;
            var assignedBy = _sampleAssignedBy;

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(assignedTo);
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(assignedBy);
            _mockAssignmentRepository.Setup(x => x.Create(It.IsAny<Assignment>())).ReturnsAsync(assignment);

            var assignmentService = new AssignmentService(_mockAssignmentRepository.Object, _mockUserRepository.Object, _mockAssetRepository.Object);
            //Act
            var testResult = await assignmentService.Create(new AssignmentRequest
            {
                AssetId = assignment.Id,
                AssignedById = assignment.AssignedById,
                AssignedDate = assignment.AssignedDate,
                AssignedToId = assignment.AssignedToId,
                Note = assignment.Note,
            });

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(assignment.Id, testResult!.Id);
            Assert.Equal(assignment.AssignedById, testResult.AssignedById);
            Assert.Equal(assignment.AssignedDate, testResult.AssignedDate);
            Assert.Equal(assignment.AssignedToId, testResult.AssignedToId);
            Assert.Equal(assignment.Note, testResult.Note);
        }

        [Theory, MemberData(nameof(FalseThrowException))]
        public async Task CreateNewAssignment_ShouldThrowException(Asset asset, User assignedTo, User assignedBy, DateTime assignedDate, List<Assignment> assignments)
        {
            ////Arrange
            //var failedSample = new Assignment
            //{
            //    Asset = asset,
            //    Id = 1,
            //    AssetId =1,
            //    AssignedBy = assignedBy,
            //    AssignedById =1,
            //    AssignedDate = assignedDate,
            //    AssignedToId = 1,
            //    AssignedTo = assignedTo,
            //    IsReturning = false,
            //    Note = "null",
            //    State = assignmentState
            //};

            var failedSample = new AssignmentRequest { };

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(assignedTo);           
            _mockAssignmentRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Assignment, bool>>>())).ReturnsAsync(assignments);

            var assignmentService = new AssignmentService(_mockAssignmentRepository.Object, _mockUserRepository.Object, _mockAssetRepository.Object);

            //Act
            Func<Task> act = () => assignmentService.Create(failedSample);

            //Assert
            var exception = await Assert.ThrowsAsync<MyCustomException>(act);
        }

        [Theory, MemberData(nameof(CorrectGetPagination))]
        public async Task GetPaginationUser_ShouldReturnNotNull(List<AssignmentStateEnum> states, string nameToQuery
                                                                , string sort, int page, int pageSize)
        {
            //Arrange
            var assignments = GetSampleAssignmentLists();

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                Sort = sort,
                ValueSearch = nameToQuery?.Trim().ToLower() ?? string.Empty,
                AssignmentStates = states.Count != 0 ? states : null,
            };

            var expectedOutput = GetExpectedPaginationAssignmentOutput(assignments, queryModel);
            var expectedCount = expectedOutput.Source?.Count() ?? 0;
            var expectedType = expectedOutput?.Source?.GetType();
            queryModel.Page = page;

            _mockAssignmentRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Assignment, bool>>>())).ReturnsAsync(assignments);

            var assignmentService = new AssignmentService(_mockAssignmentRepository.Object, _mockUserRepository.Object, _mockAssetRepository.Object);

            //Act
            var testResult = await assignmentService.GetPagination(queryModel);
            var count = testResult?.Source?.Count() ?? 0;
            var type = testResult?.Source?.GetType();

            Assert.NotNull(testResult);
            Assert.Equal(expectedCount, count);
            Assert.Equal(expectedType, type);
        }

        public Pagination<GetAssignmentResponse?> GetExpectedPaginationAssignmentOutput(List<Assignment>? assignments, PaginationQueryModel queryModel)
        {
            // search assignment by assetcode or assetname or assignee
            var nameToQuery = "";
            if (!string.IsNullOrEmpty(queryModel.ValueSearch))
            {
                nameToQuery = queryModel.ValueSearch.Trim().ToLower();

                assignments = assignments?.Where(u => u!.Asset.AssetCode!.ToLower().Contains(nameToQuery) ||
                                        u!.Asset.AssetName!.ToLower().Contains(nameToQuery) ||
                                        u!.AssignedTo.UserName!.ToLower().Contains(nameToQuery)).ToList();
            }

            //TODO: filter assignment by state or date
            if (queryModel.AssignmentStates != null)
            {
                assignments = assignments?.Where(u => queryModel.AssignmentStates.Contains(u.State))?.ToList();
            }
            if (queryModel.FilterByAssignedDates != null)
            {
                assignments = assignments?.Where(u => u.AssignedDate.Day.Equals(queryModel.FilterByAssignedDates.Value.Day)
                                                    && u.AssignedDate.Month.Equals(queryModel.FilterByAssignedDates.Value.Month)
                                                    && u.AssignedDate.Year.Equals(queryModel.FilterByAssignedDates.Value.Year)).ToList();
            }

            // sorting
            var sortOption = queryModel.Sort ??= Constants.AssignmentIdAcsending;

            switch (sortOption)
            {
                case Constants.AssignmentIdAcsending:
                    assignments = assignments?.OrderBy(u => u.Id)?.ToList();
                    break;
                case Constants.AssignmentIdDescending:
                    assignments = assignments?.OrderByDescending(u => u.Id)?.ToList();
                    break;
                case Constants.AssignmentCodeAcsending:
                    assignments = assignments?.OrderBy(u => u.Asset.AssetCode)?.ToList();
                    break;
                case Constants.AssignmentCodeDescending:
                    assignments = assignments?.OrderByDescending(u => u.Asset.AssetCode)?.ToList();
                    break;
                case Constants.AssignmentNameAcsending:
                    assignments = assignments?.OrderBy(u => u.Asset.AssetName)?.ToList();
                    break;
                case Constants.AssignmentNameDescending:
                    assignments = assignments?.OrderByDescending(u => u.Asset.AssetName)?.ToList();
                    break;
                case Constants.AssignmentAssignedToAcsending:
                    assignments = assignments?.OrderBy(u => u.AssignedTo.UserName)?.ToList();
                    break;
                case Constants.AssignmentAssignedToDescending:
                    assignments = assignments?.OrderByDescending(u => u.AssignedTo.UserName)?.ToList();
                    break;
                case Constants.AssignmentAssignedByAcsending:
                    assignments = assignments?.OrderBy(u => u.AssignedBy!.UserName)?.ToList();
                    break;
                case Constants.AssignmentAssignedByDescending:
                    assignments = assignments?.OrderByDescending(u => u.AssignedBy!.UserName)?.ToList();
                    break;
                case Constants.AssignmentAssignedDateAcsending:
                    assignments = assignments?.OrderBy(u => u.AssignedDate)?.ToList();
                    break;
                case Constants.AssignmentAssignedDateDescending:
                    assignments = assignments?.OrderByDescending(u => u.AssignedDate)?.ToList();
                    break;
                case Constants.AssignmentStateAcsending:
                    assignments = assignments?.OrderBy(u => u.State)?.ToList();
                    break;
                case Constants.AssignmentStateDescending:
                    assignments = assignments?.OrderByDescending(u => u.State)?.ToList();
                    break;
                default:
                    assignments = assignments?.OrderBy(u => u.Id)?.ToList();
                    break;
            }


            var output = new Pagination<GetAssignmentResponse>();

            output.TotalRecord = assignments.Count();

            var listAssignments = assignments.Select(ass => new GetAssignmentResponse(ass));

            output.Source = listAssignments.Skip((queryModel.Page - 1) * queryModel.PageSize)
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
