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
            
                Category = GetCategorySample()

            };
        }

        public AssetRequest GetSampleAssetRequest()
        {
            return new AssetRequest
            {
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2022, 12, 29),
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
                Category = new Category()
                {
                    Id = "MO",
                    Name = "Monitor"
                }
            },
                new Asset
            {
                Id = 3,
                AssetName = "PC Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.DN,
                Specification = "null",
                CategoryId = "PC",
                AssetCode ="PC00003",
                Category = new Category()
                {
                    Id = "PC",
                    Name = "Personal Computer"
                }
            },
                new Asset
            {
                Id = 4,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00004",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 5,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00005",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 6,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00006",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 7,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00007",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 8,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00008",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 9,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA00009",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 10,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA000010",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 11,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA000011",
                Category = GetCategorySample()

            },
                new Asset
            {
                Id = 12,
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2000, 01, 13),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA",
                AssetCode ="LA000012",
                Category = GetCategorySample()

            },

        };
        }

        public static readonly object[][] CorrectGetPagination =
       {
            //LocationEnum location, List<StaffEnum> types, string nameToQuery, string sort, int page, int pageSize
            new object[] { LocationEnum.HN, new List<AssetStateEnum>(), "", "", 1, 10, new List<string>(){"LA"} },
            new object[] { LocationEnum.HCM, new List<AssetStateEnum>(), "", "", 1, 10, new List<string>()},
            new object[] { LocationEnum.DN, new List<AssetStateEnum>(), "CACACKLKLNACKL", "", 1, 10,new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() { AssetStateEnum.Assigned}, "", "", 1, 10,new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() { AssetStateEnum.Available}, "", "", 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() { AssetStateEnum.NotAvailable, AssetStateEnum.Available}, "", "", 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() { AssetStateEnum.WaitingForRecycling, AssetStateEnum.Available}, "", "", 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() { AssetStateEnum.Recycled, AssetStateEnum.Available}, "", "", 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "l", "", 1, 10, new List<string>()},
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetNameAcsending, 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetNameDescending, 1, 10, new List<string>()},
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetCodeAcsending, 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetCodeDescending, 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetCategoryNameAcsending, 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetCategoryNameDescending, 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetStateAcsending, 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetStateDescending, 1, 10, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetStateDescending, 1, 2, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetStateDescending, 2, 2, new List<string>() },
            new object[] { LocationEnum.HN, new List<AssetStateEnum>() , "", Constants.AssetStateDescending, 100, 10, new List<string>() },
    };

        [Theory, MemberData(nameof(CorrectGetPagination))]
        public async Task GetPaginationAsset_ShouldReturnNotNull(LocationEnum location, List<AssetStateEnum> assetStates, string nameToQuery
                                                              , string sort, int page, int pageSize,List<string> categories)
        {
            //Arrange
            var assets = GetSampleAssetLists();
            var assetLocation = assets.Where(x => x.Location == location)?.ToList() ?? new List<Asset>();

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                Sort = sort,
                ValueSearch = nameToQuery?.Trim()?.ToLower() ?? string.Empty,
                AssetStates = assetStates.Count != 0 ? assetStates : null,
                Categories = categories.Count != 0 ? categories : null
            };

            var expectedOutput = GetExpectedPaginationAssetOutput(assetLocation, queryModel);
            var expectedCount = expectedOutput.Source?.Count() ?? 0;
            var expectedType = expectedOutput?.Source?.GetType();

            _mockAssetRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(assetLocation);

            queryModel.Page = page;

            var userService = new AssetService(_mockAssetRepository.Object,_mockCategoryRepository.Object);

            // act
            var testResult = await userService.GetPagination(queryModel, location);
            var count = testResult?.Source?.Count() ?? 0;
            var type = testResult?.Source?.GetType();

            Assert.NotNull(testResult);
            Assert.Equal(expectedCount, count);
            Assert.Equal(expectedType, type);
        }

        private Pagination<GetAssetResponse?> GetExpectedPaginationAssetOutput(List<Asset>? assets, PaginationQueryModel queryModel)
        {
            // filter by type
            if (queryModel.AssetStates != null)
            {
                assets = assets?.Where(u => queryModel.AssetStates.Contains(u.State))?.ToList();
            }

            if (queryModel.Categories != null)
            {
                assets = assets.Where(u => queryModel.Categories.Contains(u.CategoryId))?.ToList();
            }

            // search asset by assetcode or name
            var nameToQuery = "";
            if (!string.IsNullOrEmpty(queryModel.ValueSearch))
            {
                nameToQuery = queryModel.ValueSearch.Trim().ToLower();

                assets = assets?.Where(u => u!.AssetName!.ToLower().Contains(nameToQuery) ||
                                        u!.AssetCode!.ToLower().Contains(nameToQuery))?.ToList();
            }

            //sorting
            var sortOption = queryModel.Sort ??= Constants.AssetCodeAcsending;

            switch (sortOption)
            {
                case Constants.AssetNameAcsending:
                    assets = assets?.OrderBy(u => u.AssetName)?.ToList();
                    break;
                case Constants.AssetNameDescending:
                    assets = assets?.OrderByDescending(u => u.AssetName)?.ToList();
                    break;
                case Constants.AssetCodeAcsending:
                    assets = assets?.OrderBy(u => u.AssetCode)?.ToList();
                    break;
                case Constants.AssetCodeDescending:
                    assets = assets?.OrderByDescending(u => u.AssetCode)?.ToList();
                    break;
                case Constants.AssetCategoryNameAcsending:
                    assets = assets?.OrderBy(u => u.Category.Name)?.ToList();
                    break;
                case Constants.AssetCategoryNameDescending:
                    assets = assets?.OrderByDescending(u => u.Category.Name)?.ToList();
                    break;
                case Constants.AssetStateAcsending:
                    assets = assets?.OrderBy(u => u.State)?.ToList();
                    break;
                case Constants.AssetStateDescending:
                    assets = assets?.OrderByDescending(u => u.State)?.ToList();
                    break;
                default:
                    assets = assets?.OrderBy(u => u.AssetCode)?.ToList();
                    break;
            }

            //paging
            if (assets == null || assets.Count() == 0)
            {
                return new Pagination<GetAssetResponse?>
                {
                    Source = null,
                    TotalPage = 1,
                    TotalRecord = 0,
                    QueryModel = queryModel
                };
            }

            var output = new Pagination<GetAssetResponse>();

            output.TotalRecord = assets.Count();

            var listassets = assets.Select(asset => new GetAssetResponse(asset));

            output.Source = listassets.Skip((queryModel.Page - 1) * queryModel.PageSize)
                                    .Take(queryModel.PageSize)
                                    .ToList();

            output.TotalPage = (output.TotalRecord - 1) / queryModel.PageSize + 1;

            if (queryModel.Page > output.TotalPage)
            {
                queryModel.Page = output.TotalPage;
            }

            output.QueryModel = queryModel;

            return output;
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
            _mockAssetRepository.Setup(x => x.Create(It.IsAny<Asset>())).ReturnsAsync(asset);

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

        [Fact]
        public async Task Create_ShouldReurnAssetWithStateChange_WhenDateInFuture()
        {
            var asset = GetSampleAsset();

            var category = GetCategorySample();

            var assetRequest = new AssetRequest
            {
                AssetName = "Laptop Sample",
                InstalledDate = new DateTime(2022, 12, 3),
                State = AssetStateEnum.NotAvailable,
                Location = LocationEnum.HN,
                Specification = "null",
                CategoryId = "LA"
            };

            var expectedResult = GetExpectedResult();

            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            _mockAssetRepository.Setup(x => x.Create(It.IsAny<Asset>())).ReturnsAsync(asset);

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

        [Fact]
        public async Task GetAssetByName_ShouldReturnNull()
        {
            Asset asset = null;
            string assetName = null;

            var assetRequest = GetSampleAssetRequest();

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.GetAssetByName(assetName);

            //Arrange
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAssetByName_ShouldReturnNotNull()
        {
            Asset asset = GetSampleAsset();
            string assetName = "Laptop Sample";

            var assetRequest = GetSampleAssetRequest();

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.GetAssetByName(assetName);

            //Arrange
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ShouldNotFoundAsset()
        {
            Asset asset = null;

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.Delete(1);

            //Arrange
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue()
        {
            Asset asset = GetSampleAsset();

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            _mockAssetRepository.Setup(x => x.Delete(asset)).ReturnsAsync(true);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.Delete(1);

            //Arrange
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse()
        {
            Asset asset = GetSampleAsset();

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            _mockAssetRepository.Setup(x => x.Delete(asset)).ReturnsAsync(false);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //Act
            var result = await assetService.Delete(1);

            //Arrange
            Assert.False(result);
        }

        [Fact]
        public async Task Update_ShouldReturnNull()
        {
            var assetRequest = GetSampleAssetRequest();
            Asset asset = null;

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //act
            var result = await assetService.Update(1, assetRequest);

            //Arrange
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldReturnNullUpdate()
        {
            var assetRequest = GetSampleAssetRequest();
            Asset asset = null;

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            _mockAssetRepository.Setup(x => x.Update(It.IsAny<Asset>())).ReturnsAsync(asset);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //act
            var result = await assetService.Update(1, assetRequest);

            //Arrange
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedAsset()
        {
            var assetRequest = GetSampleAssetRequest();
            Asset asset = GetSampleAsset();

            var expectedResult = GetExpectedResult();

            _mockAssetRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(asset);
            _mockAssetRepository.Setup(x => x.Update(It.IsAny<Asset>())).ReturnsAsync(expectedResult);

            var assetService = new AssetService(_mockAssetRepository.Object, _mockCategoryRepository.Object);

            //act
            var result = await assetService.Update(1, assetRequest);

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

    }
}
