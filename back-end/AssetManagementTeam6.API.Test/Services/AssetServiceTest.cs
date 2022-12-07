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

        public Asset GetExpectedResult()
        {
            return new Asset
            {
                Id = 1,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                //AssetCode = "LA00001",
                Category = GetCategorySample()
                
            };
        }

        public AssetRequest GetSampleAssetRequest() { 
            return new AssetRequest
            {
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
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
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                //AssetCode = "LA00001",
                Category = GetCategorySample()
                
            };
        }

        public List<Asset> GetSampleAssetLists()
        {
            return new List<Asset>
            {
                new Asset
            {
                Id = 1,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00001",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 2,
                AssetName = "Monitor Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "MO",
                AssetCode ="MO00002",
                Category = GetCategorySample()
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

            var expectedResult = GetExpectedResult();
            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act 
            var result = await assetService.GetAssetById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.AssetName, result.AssetName);
            Assert.Equal(expectedResult.Specification, result.Specification);
            Assert.Equal(expectedResult.Location, result.Location);
            Assert.Equal(expectedResult.CategoryId, result.CategoryId);
            Assert.Equal(expectedResult.State, result.State);
            Assert.Equal(expectedResult.InstalledDate, result.InstalledDate);
            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.AssetCode, result.AssetCode);
            Assert.Equal(expectedResult.Category.Id, result.Category.Id);
            Assert.Equal(expectedResult.Category.Name, result.Category.Name);

        }

        [Fact]
        public async Task Create_ShouldReturnCategoryNull()
        {
            Category category = null;

            var assetRequest = GetSampleAssetRequest();

            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            //_mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.Create(assetRequest);

            //Arrange
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldReturnNull()
        {
            Asset asset = null;

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

            var expectedResult = GetExpectedResult();

            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.Create(assetRequest);

            //Arrange
            Assert.NotNull(result);
            Assert.Equal(expectedResult.AssetName, result.AssetName);
            Assert.Equal(expectedResult.Specification, result.Specification);
            Assert.Equal(expectedResult.Location, result.Location);
            Assert.Equal(expectedResult.CategoryId, result.CategoryId);
            Assert.Equal(expectedResult.State, result.State);
            Assert.Equal(expectedResult.InstalledDate, result.InstalledDate);
            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.AssetCode, result.AssetCode);
            Assert.Equal(expectedResult.Category.Id, result.Category.Id);
            Assert.Equal(expectedResult.Category.Name, result.Category.Name);
        }

        //TODO : State
        [Theory]
        [InlineData(LocationEnum.HN)]
        [InlineData(LocationEnum.HCM)]
        [InlineData(LocationEnum.DN)]
        public async Task GetAll_ShouldReturnNull(LocationEnum location)
        {
            var asset = new List<Asset>();       

            var assetLocation = asset?.Where(x => x.Location == location).ToList() ?? new List<Asset>();
            _mockAssetRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(assetLocation);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.GetAllAsync(location);

            //Arrange
            Assert.Null(result);
        }

        [Theory]
        [InlineData(LocationEnum.HN)]
        public async Task GetAll_ShouldReturnNotNull(LocationEnum location)
        {
            var assetList = GetSampleAssetLists();

            var expectedResult = new List<Asset>
            {
                new Asset
            {
                Id = 1,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00001",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 2,
                AssetName = "Monitor Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "MO",
                AssetCode ="MO00002",
                Category = GetCategorySample()
            }
            };

            _mockAssetRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(assetList);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.GetAllAsync(location);

            //Arrange
            Assert.NotNull(result);
            Asset.Equals(result.Count(), expectedResult.Count());
        }


    }
}
