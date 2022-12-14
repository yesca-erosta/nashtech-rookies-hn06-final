using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.ErrorHandling;
using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
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


        public static readonly object[][] FalseThrowException =
{
            //Asset asset, User assignedTo, User assignedBy, DateTime assignedDate, List<Assignment> assignments
            //new object[] { null!, new User(), new User(), DateTime.UtcNow, new List<Assignment>()},
            //new object[] { _assetSampleFailed, new User(), new User(), DateTime.UtcNow, new List<Assignment>() },
            new object[] { _asset, null!, new User(), DateTime.UtcNow, new List<Assignment>() },
            //new object[] { null, AssetStateEnum.Available, AssignmentStateEnum.WaitingForAcceptance, new User(), new User(), DateTime.UtcNow },
            //new object[] { null, AssetStateEnum.Available, AssignmentStateEnum.WaitingForAcceptance, new User(), new User(), DateTime.UtcNow },
            //new object[] { null, AssetStateEnum.Available, AssignmentStateEnum.WaitingForAcceptance, new User(), new User(), DateTime.UtcNow },

        };

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
    }
}
