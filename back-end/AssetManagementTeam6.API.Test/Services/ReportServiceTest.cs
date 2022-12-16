using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Enums;
using Moq;
using System.Linq.Expressions;

namespace AssetManagementTeam6.API.Test.Services
{
    public class ReportServiceTest
    {
        public Mock<IReportRepository> _mockReportRepository;
        public Mock<ICategoryRepository> _mockCategoryRepository;
        public Mock<IAssetRepository> _mockAssetRepository;
        public ReportServiceTest()
        {
            _mockReportRepository = new Mock<IReportRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockAssetRepository = new Mock<IAssetRepository>();
        }

        public List<Category> GetSampleCategoryLists()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = "LA",
                    Name = "Laptop"
                },
                new Category
                {
                    Id = "MO",
                    Name = "Mornitor"
                },
                new Category
                {
                    Id = "PC",
                    Name = "Personal computer"
                }
            };          
        }

        public Category GetCategorySample()
        {
            return new Category
            {
                Id = "LA",
                Name = "Laptop"
            };
        }

        public Report GetReportSample()
        {
            return new Report
            {
                CategoryId = "LA",
                Category = new Category
                {
                    Id = "LA",
                    Name = "Laptop"
                },
                Assigned = 10,
                Available = 1,
                Id = 1,
                NotAvailable = 0,
                Recycled = 0,
                Total = 11,
                WaitingForRecycling = 0
            };
        }

        public List<Report> GetSampleReportLists()
        {
            return new List<Report>
            {
                new Report
                {
                    CategoryId = "LA",
                    Category =  new Category
                    {
                        Id = "LA",
                        Name = "Laptop"
                    },
                    Assigned = 10,
                    Available = 1,
                    Id = 1,
                    NotAvailable = 0,
                    Recycled = 0,
                    Total = 11,
                    WaitingForRecycling = 0
                },
                new Report
                {
                    CategoryId = "MO",
                    Category =  new Category
                    {
                        Id = "MO",
                        Name = "Mornitor"
                    },
                    Assigned = 10,
                    Available = 1,
                    Id = 1,
                    NotAvailable = 0,
                    Recycled = 0,
                    Total = 11,
                    WaitingForRecycling = 0
                },
                new Report
                {
                    CategoryId = "PC",
                    Category =  new Category
                    {
                        Id = "PC",
                        Name = "Personal computer"
                    },
                    Assigned = 10,
                    Available = 1,
                    Id = 1,
                    NotAvailable = 0,
                    Recycled = 0,
                    Total = 11,
                    WaitingForRecycling = 0
                },
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


        [Fact]
        public async Task GetListReport_ShouldNotReturnNull()
        {
            //Arrange
            var categories = GetSampleCategoryLists();
           // var reports = GetSampleReportLists();
            var assets = GetSampleAssetLists();
            _mockCategoryRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(categories);
            _mockAssetRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(assets);
            //_mockAssetRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(
            //            (Expression<Func<Asset, bool>> predicate) => assets.Where(predicate.Compile())
            //);
            var reportService = new ReportService(_mockReportRepository.Object, _mockCategoryRepository.Object, _mockAssetRepository.Object);
            //Act
            var testResult = await reportService.GetListReport();

            //Arrange
            Assert.NotEmpty(testResult);
        }

        [Fact]
        public async Task CreateReport_ShouldNotReturnNull()
        {
            //Arrange
            var reports = GetSampleReportLists();
            var report = GetReportSample();
            var categories = GetSampleCategoryLists();
            var assets = GetSampleAssetLists();
            _mockCategoryRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(categories);
            _mockAssetRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Asset, bool>>>())).ReturnsAsync(assets);
            _mockReportRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Report, bool>>>())).ReturnsAsync(reports);
            _mockReportRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Report, bool>>>())).ReturnsAsync(report);

            var createRequestList = GetListReport(categories, assets);
            var reportService = new ReportService(_mockReportRepository.Object, _mockCategoryRepository.Object, _mockAssetRepository.Object);

            //Act
            var testResult = await reportService.CreateReport(createRequestList);

            //Arrange
            Assert.Null(testResult);
        }

        private IEnumerable<Report> GetListReport(List<Category> categories, List<Asset> assets)
        {

            var reports = new List<Report>();

            foreach (var cat in categories)
            {
                var asset =  assets.Where(a => a.CategoryId == cat.Id);
                var assetAssigned = assets.Where(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.Assigned);
                var assetAvailable = assets.Where(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.Available);
                var assetWFR = assets.Where(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.WaitingForRecycling);
                var assetRecycled = assets.Where(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.Recycled);
                var assetNotAvailable = assets.Where(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.NotAvailable);

                var report = new Report
                {
                    CategoryId = cat.Id,
                    Category = cat,
                    Total = asset.Count(),
                    Assigned = assetAssigned.Count(),
                    Available = assetAvailable.Count(),
                    WaitingForRecycling = assetWFR.Count(),
                    Recycled = assetRecycled.Count(),
                    NotAvailable = assetNotAvailable.Count()
                };

                reports.Add(report);
            }

            return reports;
        }
    }
}
