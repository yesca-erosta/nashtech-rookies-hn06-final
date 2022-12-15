using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
using Common.Enums;
using Moq;
using System.Linq.Expressions;

namespace AssetManagementTeam6.API.Test.Services.ToRemove
{
    public class ToRemoveServiceTest
    {
        private Mock<IAssignmentRepository> _mockAssignmentRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IAssetRepository> _mockAssetRepository;

        public ToRemoveServiceTest()
        {
            _mockAssignmentRepository = new Mock<IAssignmentRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockAssetRepository = new Mock<IAssetRepository>();
        }

        [Fact]
        public async Task Create_ShouldBeOK()
        {
            //ARRANGE
            var listUser = TestBase.ReadJsonFromFile<List<User>>("dummy_user_data.json");

            var expectedString01 = $"assignedTo: dummyuser1 - assignedBy: dummyuser2";
            var expectedString02 = $"assignedTo: dummyuser2 - assignedBy: dummyuser1";
            var expectedString03 = "Data invalid";

            _mockUserRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(
                    (Expression<Func<User, bool>> predicate) => listUser.FirstOrDefault(predicate.Compile())
            );

            var removeService = new RemoveService(_mockAssignmentRepository.Object, _mockUserRepository.Object, _mockAssetRepository.Object);

            //ACT
            var string01 = await removeService.CreateTestXyz(1, 2);
            var string02 = await removeService.CreateTestXyz(2, 1);
            var string03 = await removeService.CreateTestXyz(100000, 999999);

            //ASSERT
            Assert.Equal(expectedString01, string01);
            Assert.Equal(expectedString02, string02);
            Assert.Equal(expectedString03, string03);
        }
    }
}
