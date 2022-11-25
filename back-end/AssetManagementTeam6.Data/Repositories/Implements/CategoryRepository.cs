using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AssetManagementContext context) : base(context)
        {

        }
    }
}
