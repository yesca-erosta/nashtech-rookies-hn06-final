using AssetManagementTeam6.API.Dtos.Requests;
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

        public AssignmentServiceTest()
        {
            _mockAssignmentRepository = new Mock<IAssignmentRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockAssetRepository = new Mock<IAssetRepository>();
        }

        [Fact]
        public async Task CreateNewAssignment_ShouldNotReturnNull()
        {
            //Arrange
            var assignment = new Assignment
            {
                AssetId = 1,
                AssignedToID = 2,
                AssignedById = 1,
                AssignedDate = DateTime.UtcNow,
                State = AssignmentStateEnum.WaitingForAcceptance,
                Note = "Unit Test"
            };

            var asset = new Asset
            {
                Id = 1
            };

            var assignedTo = new User
            {
                Id = 2
            };

            var assignedBy = new User
            {
                Id = 1
            };

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
                AssignedToID = assignment.AssignedToID,
                Note = assignment.Note,
            });

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(assignment.Id, testResult!.Id);
            Assert.Equal(assignment.AssignedById, testResult.AssignedById);
            Assert.Equal(assignment.AssignedDate, testResult.AssignedDate);
            Assert.Equal(assignment.AssignedToID, testResult.AssignedToID);
            Assert.Equal(assignment.Note, testResult.Note);
        }
    }
}
