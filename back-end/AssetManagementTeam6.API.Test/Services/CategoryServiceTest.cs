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
    public class CategoryServiceTest
    {
        private Mock<ICategoryRepository> _mockCategoryRepository;
        public CategoryServiceTest()
        {
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
        public Category GetExpectedResult()
        {
            return new Category
            {
                Id = "LA",
                Name = "Laptop"
            };
        }

        [Fact]
        public async Task getCategoryById_ShouldReturnNull()
        {
            //Arrange
            Category? category = null;

            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            var categoryService = new CategoryService(_mockCategoryRepository.Object);

            //Act
            var result = await categoryService.GetCategoryById("LA");

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task getCategoryById_ShouldReturnNotNull()
        {
            var category = GetCategorySample();

            var expectedResult = GetExpectedResult();
            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            var categoryService = new CategoryService( _mockCategoryRepository.Object);

            //Act 
            var result = await categoryService.GetCategoryById("LA");

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Name, result.Name);
            Assert.Equal(expectedResult.Id, result.Id);
        }

        [Fact]
        public async Task getCategoryByName_ShouldReturnNull()
        {
            //Arrange
            Category? category = null;

            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            var categoryService = new CategoryService(_mockCategoryRepository.Object);

            //Act
            var result = await categoryService.GetCategoryByName("laptop");

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task getCategoryByName_ShouldReturnNotNull()
        {
            var category = GetCategorySample();

            var expectedResult = GetExpectedResult();
            _mockCategoryRepository.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            var categoryService = new CategoryService(_mockCategoryRepository.Object);

            //Act 
            var result = await categoryService.GetCategoryByName("laptop");

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Name, result.Name);
            Assert.Equal(expectedResult.Id, result.Id);
        }

        [Fact]
        public async Task Create_ShouldReturnNull()
        {
            Category category = null;

            var request = new CategoryRequest
            {
                Id = "LA",
                Name = "laptop"
            };

            _mockCategoryRepository.Setup(x => x.Create(It.IsAny<Category>())).ReturnsAsync(category);

            var categoryService = new CategoryService(_mockCategoryRepository.Object);

            //Act
            var result = await categoryService.Create(request);

            //Arrange
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldReturnNotNull()
        {
            var category = GetCategorySample();

            var request = new CategoryRequest
            {
                Id = "LA",
                Name = "Laptop"
            };

            var expectedResult = GetExpectedResult();

            _mockCategoryRepository.Setup(x => x.Create(It.IsAny<Category>())).ReturnsAsync(category);

            var categoryService = new CategoryService(_mockCategoryRepository.Object);

            //Act
            var result = await categoryService.Create(request);

            //Arrange
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Name, result!.Name);
            Assert.Equal(expectedResult.Id, result.Id);
        }

        [Fact]
        public async Task getListCategory_ShouldReturnNull()
        {
            //Arrange
            var category = new List<Category>();

            _mockCategoryRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);

            var categoryService = new CategoryService(_mockCategoryRepository.Object);

            //Act
            var result = await categoryService.GetAllAsync();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task getListCategory_ShouldReturnNotNull()
        {
            var category = new List<Category>()
            {
                new Category
                {
                    Id = "LA",
                    Name = "Laptop"
                },
                  new Category
                {
                    Id = "MO",
                    Name = "Monior"
                },
                    new Category
                {
                    Id = "PC",
                    Name = "Personal Computer"
                },
                      new Category
                {
                    Id = "ME",
                    Name = "Mouse Esport"
                }
            };

            _mockCategoryRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);
            var categoryService = new CategoryService(_mockCategoryRepository.Object);

            //Act 
            var result = await categoryService.GetAllAsync();

            //Assert
            Assert.NotNull(result!);
            Assert.Equal(result!.Count(), 4);
            Assert.IsType<List<Category>>(result!);
        }
    }
}
