using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    [ExcludeFromCodeCoverage]
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AssetManagementContext context) : base(context)
        {

        }
    }
}
