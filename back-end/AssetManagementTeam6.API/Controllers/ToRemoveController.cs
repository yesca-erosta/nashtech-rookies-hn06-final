using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.ErrorHandling;
using AssetManagementTeam6.API.Heplers;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using Common.Constants;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace AssetManagementTeam6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
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

        [HttpGet("assignment")]
        public IActionResult FakeAssignment()
        {
            var output = new List<Assignment>();
            for (int i = 0; i < 35; i++)
            {
                output.Add(new Assignment
                {
                    AssignedDate = DateTime.Now,
                    Note = $"Assignment {i}",
                    IsReturning = i%2==0 ? true : false,
                    State = (AssignmentStateEnum) (i%3),
                    Id = i,
                    AssetId = 1,
                    AssignedToId = 2,
                    AssignedById = 1,
                    Asset = new Asset
                    {
                        Id = 1,
                        AssetName = $"Laptop Sample {i}",
                        InstalledDate = new DateTime(2000, 01, 13),
                        State = AssetStateEnum.NotAvailable,
                        Location = LocationEnum.HN,
                        Specification = "null",
                        CategoryId = "LA",

                        Category = new Category
                        {
                            Id = "LA",
                            Name = "Laptop"
                        }
                    },
                    AssignedBy = new User
                    {
                        Id = 1,
                        UserName = $"dongnp13{i}",
                        FirstName = "Dong",
                        LastName = "Nguyen Phuong",
                        DateOfBirth = new DateTime(2000, 01, 13),
                        Gender = GenderEnum.Male,
                        JoinedDate = new DateTime(2010, 10, 10),
                        Location = LocationEnum.HN,
                        NeedUpdatePwdOnLogin = true,
                        Password = SystemFunction.CreateMD5("Admin@123"),
                        Type = StaffEnum.Admin,
                    },
                    AssignedTo = new User
                    {
                        Id = 2,
                        UserName = $"tineship{i}",
                        FirstName = "Tien",
                        LastName = "Nguyen",
                        DateOfBirth = new DateTime(1999, 01, 13),
                        Gender = GenderEnum.Male,
                        JoinedDate = new DateTime(2012, 10, 10),
                        Location = LocationEnum.HN,
                        NeedUpdatePwdOnLogin = true,
                        Password = SystemFunction.CreateMD5("Admin@123"),
                        Type = StaffEnum.Staff,
                    }
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
