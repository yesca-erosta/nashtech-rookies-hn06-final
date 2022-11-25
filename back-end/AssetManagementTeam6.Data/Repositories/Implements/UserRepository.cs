using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        // userrepository extend base co phuong thuc cua base, crud iuserrepo và  
        public UserRepository(AssetManagementContext context) : base(context)
        {

        }

        public void Nguvcl()
        {
            throw new NotImplementedException();
        }
    }
}
