using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Enums;
using Moq;
using System.Linq.Expressions;

namespace AssetManagementTeam6.API.Test.Services
{
    public class AssetServiceTest
    {
        private Mock<IAssetRepository> _mockAssetRepository;
        private Mock<ICategoryRepository> _mockCategoryRepository;
        public AssetServiceTest()
        {
            _mockAssetRepository = new Mock<IAssetRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
        }

        public Category GetCategorySample()
        {
            return new Category
            {
                Id = "LA",
                Name = "Laptop"
            };
        }

        public AssetRequest GetSampleAssetRequest() { 
            return new AssetRequest
            {
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = StateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA"
            };
        }

        public Asset GetSampleAsset()
        {
            return new Asset
            {
                Id = 1,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = StateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode = "LA00001",
                Category =
                {
                    Id = "LA",
                    Name = "Laptop"
                }
            };
        }

        public IEnumerable<Asset> GetSampleAssetLists()
        {
            return new List<Asset>
            {
                new Asset
            {
                Id = 1,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = StateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00001",
                Category =
                {
                    Id = "LA",
                    Name = "Laptop"
                }
            },
                new Asset
            {
                Id = 2,
                AssetName = "Monitor Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = StateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "MO",
                AssetCode ="MO00002",
                Category =
                {
                    Id = "MO",
                    Name = "Monitor"
                }
            },new Asset
            {
                Id = 3,
                AssetName = "PC Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = StateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "PC",
                AssetCode = "PC00003",
                Category =
                {
                    Id = "PC",
                    Name = "Personal computer"
                }
            }
        };
        }

        [Fact]
        public async Task getAssetById_ShouldReturnNull()
        {
            //Arrange
            Asset? asset = null;

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.GetAssetById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task getAssetById_ShouldReturnNotNull()
        {
            var asset = GetSampleAsset();
            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act 
            var result = await assetService.GetAssetById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(asset.AssetName, result.AssetName);
            Assert.Equal(asset.Specification, result.Specification);
            Assert.Equal(asset.Location, result.Location);
            Assert.Equal(asset.CategoryId, result.CategoryId);
            Assert.Equal(asset.State, result.State);
            Assert.Equal(asset.InstalledDate, result.InstalledDate);
            Assert.Equal(asset.Id, result.Id);
            Assert.Equal(asset.AssetCode, result.AssetCode);
            Assert.Equal(asset.Category, result.Category); 
        }

        [Fact]
        public async Task Create_ShouldReturnNull()
        {
            var asset = GetSampleAsset();

            var category = GetCategorySample();

            var assetRequest = GetSampleAssetRequest();

            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.Create(assetRequest);

            //Arrange
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldReturnNotNull()
        {
            var asset = GetSampleAsset();

            var category = GetCategorySample();

            var assetRequest = GetSampleAssetRequest();

            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.Create(assetRequest);

            //Arrange
            Assert.Null(result);
        }
    }
}
