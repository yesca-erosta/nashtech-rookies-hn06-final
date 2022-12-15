using AssetManagementTeam6.API.Attributes;
using AssetManagementTeam6.API.Reports;
using AssetManagementTeam6.API.Services.Interfaces;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> CreateReport()
        {
            await _reportService.CreateReport();

            return Ok();
        }

        [HttpGet()]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> GetReportResult()
        {
            var result = await _reportService.GetAll();

            return Ok(result);
        }

        [HttpPost("export")]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<ActionResult> Export()
        {
            var report = await _reportService.GetAll();
            var memoryStream = new CustomExportExcel().ExportDataToStream(report.ToList());
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"Report-{DateTime.UtcNow}.xlsx";
            return File(memoryStream.ToArray(), contentType, fileName);
        }

    }
}
