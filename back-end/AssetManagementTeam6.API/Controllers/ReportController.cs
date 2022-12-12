using AssetManagementTeam6.API.Attributes;
using AssetManagementTeam6.API.Services.Interfaces;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text;

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
            var obj = await _reportService.GetAll();
            StringBuilder str = new StringBuilder();
            str.Append("<table border=`" + "1px" + "`b>");
            str.Append("<tr>");
            str.Append("<td><b><font face=Arial Narrow size=3>Category</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Total</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Assigned</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Available</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Not available</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Waiting for recycling</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Recycled</font></b></td>");
            str.Append("</tr>");
            foreach (var val in obj)
            {
                str.Append("<tr>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Category.Name.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Total.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Assigned.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Available.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.NotAvailable.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.WaitingForRecycling.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Recycled.ToString() + "</font></td>");
                str.Append("</tr>");
            }
            str.Append("</table>");

            HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=Report" + DateTime.Now.ToString() + ".xlsx");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel");
        }
    }
}
