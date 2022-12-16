using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    [ExcludeFromCodeCoverage]
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AssetManagementContext context) : base(context)
        {

        }
    }
}
