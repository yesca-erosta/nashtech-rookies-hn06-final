using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAll();
        Task<Report> CreateReport(IEnumerable<Report> createRequestList);
        Task<IEnumerable<Report>> GetListReport();
    }
}
