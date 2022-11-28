using AssetManagementTeam6.API.Attributes;
using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using Common.Enums;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementTeam6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> GetAllAsync()
        {
            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();

            var user = await _userService.GetUserById(userId.Value);

            var location = user.Location;

            try
            {
                var entities = await _userService.GetAllAsync(location);

                return new JsonResult(entities);
            }
            catch
            {
                return BadRequest("Bad request");
            }
        }

        [AuthorizeRoles(StaffRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody][CustomizeValidator(RuleSet = "default, CreateUser")] UserRequest requestModel)
        {
            var isExistedUser = await _userService.GetUserByUserAccount(requestModel.UserName);

            if (isExistedUser != null)
                return StatusCode(409, $"The user name {requestModel.UserName} has already existed in the system");

            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();

            var user = await _userService.GetUserById(userId.Value);

            requestModel.Location = user.Location;

            var result = await _userService.Create(requestModel);

            if (result == null)
                return StatusCode(500, "Sorry the Request failed");

            result.Password = null!;

            return Ok(result);
        }

        [AuthorizeRoles(StaffRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody][CustomizeValidator(RuleSet = "default, UpdateUser")] UserRequest requestModel)
        {

            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();

            var userAdmin = await _userService.GetUserById(userId.Value);

            if (id < 0)
            {
                return BadRequest("Invalid user");
            }
            
            requestModel.Location = userAdmin!.Location;

            var result = await _userService.Update(id, requestModel);
           
            if (result == null)
                return StatusCode(500, "Sorry the Request failed");

            result.Password = null;

            return Ok(result);
        }

        [AuthorizeRoles(StaffRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
                return StatusCode(500, "Can't found user in the system");

            await _userService.Delete(id);

            return Ok();
        }

        [HttpGet("query")]
        public async Task<IActionResult> Pagination(int page, int pageSize, string? valueSearch, string? types, string? sort)
        {
            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();

            var user = await _userService.GetUserById(userId.Value);

            var location = user!.Location;

            var listTypes = new List<StaffEnum>();

            if (!string.IsNullOrWhiteSpace(types)) 
               {
                var typeArr = types.Split(",");
                foreach (string typeValue in typeArr)
                {
                    var tryParseOk = (Enum.TryParse(typeValue, out StaffEnum enumValue)) ;
                    if (tryParseOk)
                        listTypes.Add(enumValue); ;
                }
            }

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                StaffCodeOrName = valueSearch,
                Types = listTypes.Count != 0 ? listTypes : null,
                Sort = sort
            };

            var result = await _userService.GetPagination(queryModel, location);

            return Ok(result);
        }
    }
}
