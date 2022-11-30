using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryById(string id);
        Task<Category> GetCategoryByName(string name);
        Task<Category?> Create(CategoryRequest createRequest);
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
