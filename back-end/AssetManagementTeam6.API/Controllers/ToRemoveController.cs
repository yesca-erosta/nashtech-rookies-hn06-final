using AssetManagementTeam6.Data.Entities;
using Common.Constants;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementTeam6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToRemoveController : ControllerBase
    {
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
    }
}
