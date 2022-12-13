using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAll();
        Task CreateReport();
        Task<IEnumerable<Report>> GetListReport();
    }
}
