using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class RequestForReturnRepository : BaseRepository<RequestForReturn>, IRequestForReturnRepository
    {
        public RequestForReturnRepository(AssetManagementContext context) : base(context)
        {

        }
    }
}
