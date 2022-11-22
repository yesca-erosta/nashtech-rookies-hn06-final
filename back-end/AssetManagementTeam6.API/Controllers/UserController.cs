using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementTeam6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] User requestModel)
        {
            var result = await _userService.Create(requestModel);

            if (result == null) return BadRequest();
            result.Password = null!;
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody][CustomizeValidator(RuleSet = "UpdateUser")] User requestModel)
        {
            //var result = await _userService.Create(requestModel);

            //if (result == null) return BadRequest();

            //return Ok(result);
            return NotFound();
        }
        
    }
}
