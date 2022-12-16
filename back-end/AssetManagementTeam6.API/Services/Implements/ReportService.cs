using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Implements
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAssetRepository _assetRepository;
        public ReportService(IReportRepository reportRepository, ICategoryRepository categoryRepository, IAssetRepository assetRepository)
        {
            _reportRepository = reportRepository;
            _categoryRepository = categoryRepository;
            _assetRepository = assetRepository;
        }

        public async Task<Report> CreateReport(IEnumerable<Report> createRequestList)
        {
            var reports = await _reportRepository.GetListAsync();
            //IEnumerable<Report> createRequestList = await GetListReport();

            if (reports == null || reports.Count() == 0)
            {
                foreach (var createRequest in createRequestList)
                {
                    var result = await _reportRepository.Create(createRequest);

                    return result;
                }
            }
            else
            {
                foreach (var createRequest in createRequestList)
                {
                    var reportRecord = await _reportRepository.GetOneAsync(r => r.CategoryId == createRequest.CategoryId);

                    if (reportRecord == null)
                    {
                        await _reportRepository.Update(createRequest);
                    }
                    else
                    {
                        if(reportRecord.Total != createRequest.Total)
                        {
                            reportRecord.Total = createRequest.Total;
                            reportRecord.Assigned = createRequest.Assigned;
                            reportRecord.Available = createRequest.Available;
                            reportRecord.WaitingForRecycling = createRequest.WaitingForRecycling;
                            reportRecord.Recycled = createRequest.Recycled;
                            reportRecord.NotAvailable = createRequest.NotAvailable;

                            var result = await _reportRepository.Update(reportRecord);

                            return result;
                        };
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<Report>> GetAll()
        {
            return await _reportRepository.GetListAsync();
        }

        public async Task<IEnumerable<Report>> GetListReport()
        {
            var assets = await _assetRepository.GetListAsync();
            var categories = await _categoryRepository.GetListAsync();
            var reports = new List<Report>();
           
            foreach (var cat in categories)
            {
                var asset = await _assetRepository.GetListAsync(a => a.CategoryId == cat.Id);
                var assetAssigned = await _assetRepository.GetListAsync(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.Assigned);
                var assetAvailable = await _assetRepository.GetListAsync(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.Available);
                var assetWFR = await _assetRepository.GetListAsync(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.WaitingForRecycling);
                var assetRecycled = await _assetRepository.GetListAsync(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.Recycled);
                var assetNotAvailable = await _assetRepository.GetListAsync(a => a.CategoryId == cat.Id && a.State == AssetStateEnum.NotAvailable);

                var report = new Report
                {
                    CategoryId = cat.Id,
                    Category = cat,
                    Total = asset.Count(),
                    Assigned = assetAssigned.Count(),
                    Available = assetAvailable.Count(),
                    WaitingForRecycling = assetWFR.Count(),
                    Recycled = assetRecycled.Count(),
                    NotAvailable = assetNotAvailable.Count()
                };

                reports.Add(report);
            }

            return reports;
        }
    }
}
