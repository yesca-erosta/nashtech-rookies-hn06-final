
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;


namespace AssetManagementTeam6.API.Services.Implements
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category?> Create(Category createRequest)
        {
            if (createRequest == null) return null;
            return await _categoryRepository.Create(createRequest);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetListAsync();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            return await _categoryRepository.GetOneAsync(x=> x.Id ==id);
        }
    }
}