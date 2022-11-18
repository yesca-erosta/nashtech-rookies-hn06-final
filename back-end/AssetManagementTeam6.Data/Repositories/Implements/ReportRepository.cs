using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(AssetManagementContext context) : base(context)
        {

        }
    }
}
