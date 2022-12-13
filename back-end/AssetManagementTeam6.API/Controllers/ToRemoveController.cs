using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.ErrorHandling;
using AssetManagementTeam6.API.Heplers;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using Common.Constants;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AssetManagementTeam6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToRemoveController : ControllerBase
    {
        private readonly IRemoveService _removeService;
        private readonly IUserProvider _userProvider;
        private readonly IUserService _userService;

        public ToRemoveController(IRemoveService removeService, IUserProvider userProvider, IUserService userService)
        {
            _removeService = removeService;
            _userProvider = userProvider;
            _userService = userService;
        }

        [HttpGet("user")]
        public IActionResult FakeUser()
        {
            var output = new List<User>();
            for (int i = 0; i < 35; i++)
            {
                output.Add(new User
                {
                    Id = i + 1,
                    FirstName = $"User",
                    LastName = $"Dummy",
                    UserName = $"dummyuser{i+1}",
                    DateOfBirth = new DateTime(2000, 01, 01),
                    Password = SystemFunction.CreateMD5("Admin@123"),
                    JoinedDate = new DateTime(2022, 01, 01),
                    Type = i > 29 ? StaffEnum.Admin : StaffEnum.Staff,
                    Gender = (GenderEnum)(i%3),
                    Location = (LocationEnum)(i%3),
                    NeedUpdatePwdOnLogin = false,
                });
            };
            return Ok(output);
        }

        [HttpPost("assigment")]
        public async Task<IActionResult> CreateNewAssigment([FromBody] AssignmentRequest requestModel)
        {
            var userId = _userProvider.GetUserId();

            if (userId == null)
                throw new MyCustomException(HttpStatusCode.Forbidden, "User not found");

            var user = await _userService.GetUserById(userId.Value);

            if (user == null)
                throw new MyCustomException(HttpStatusCode.Forbidden, "User not found");
            
            requestModel.AssignedById = user.Id;

            var result = await _removeService.Create(requestModel);

            return Ok(result);
        }

        [HttpPut("assigment")]
        public async Task<IActionResult> UpdateAssigment([FromBody] AssignmentRequest requestModel)
        {
            var userId = _userProvider.GetUserId();
            if (userId == null)
                throw new MyCustomException(HttpStatusCode.Forbidden, "User not found");

            var user = await _userService.GetUserById(userId.Value);
            if (user == null)
                throw new MyCustomException(HttpStatusCode.Forbidden, "User not found");

            requestModel.AssignedById = user.Id;

            var result = await _removeService.Update(requestModel);

            return Ok(result);
        }

        //[HttpGet("excel-example")]
        //public IActionResult ExportExcel()
        //{
        //    var listData = _removeService.GetExportExcelData();
        //    var memoryStream = new ExportExcel().ExportDataToStreamForSampleReport(listData.ToList);
        //    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    var fileName = $"SampleReport-{Guid.NewGuid()}.xlsx";
        //    return File(memoryStream.ToArray(), contentType, fileName);
        //}
    }
}
