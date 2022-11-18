using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AssetManagementContext context) : base(context)
        {

        }
    }
}
