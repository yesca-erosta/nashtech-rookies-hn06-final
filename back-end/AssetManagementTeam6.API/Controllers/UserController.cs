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
        public async Task<IActionResult> CreateAsync([FromBody][CustomizeValidator(RuleSet = "default, CreateUser")] User requestModel)
        {
            var user = await _userService.GetUserByUserAccount(requestModel.UserName);

            if (user != null)
                return StatusCode(409, $"User name {requestModel.UserName} has already existed in the system");

            var result = await _userService.Create(requestModel);

            if (result == null) 
                return StatusCode(500, "Sorry the Request failed");

            result.Password = null!;

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody][CustomizeValidator(RuleSet = "default, UpdateUser")] User requestModel)
        {
            var result = await _userService.Update(requestModel);

            if (result == null)
                return StatusCode(500, "Sorry the Request failed");

            return Ok();
        }

        [HttpDelete("{id}")]        
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
                return StatusCode(500, "Can't found user in the system");

            await _userService.Delete(id);

            return Ok();
        }
        
    }
}
