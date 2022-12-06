using AssetManagementTeam6.API.Attributes;
using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Heplers;
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
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IUserService _userService;
        private readonly IUserProvider _userProvider;
        public AssignmentController(IAssignmentService assignmentService, IUserService userService, IUserProvider userProvider)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _userProvider = userProvider;
        }

        [HttpPost]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> CreateAsync([FromBody] AssignmentRequest requestModel)
        {
            var userId = _userProvider.GetUserId();

            if (userId == null)
                return NotFound();

            var user = await _userService.GetUserById(userId.Value);

            if (user == null) return StatusCode(500, "Sorry the request failed");

            requestModel.AssignedById = user.Id;

            var result = await _assignmentService.Create(requestModel);

            if (result == null)
                return StatusCode(500, "Sorry the Request failed");

            return Ok(result);
        }

        [HttpGet]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> GetAll()
        {
           try
            {
                var entities = await _assignmentService.GetAllAsync();

                return Ok(entities);
            }
            catch
            {
                return BadRequest("Bad request");
            }
        }

        [HttpGet("query")]
        public async Task<IActionResult> Pagination(int page, int pageSize, string? valueSearch, string? types, string? sort)
        {
            var listTypes = new List<AssignmentStateEnum>();

            if (!string.IsNullOrWhiteSpace(types))
            {
                var typeArr = types.Split(",");
                foreach (string typeValue in typeArr)
                {
                    var tryParseOk = (Enum.TryParse(typeValue, out AssignmentStateEnum enumValue));
                    if (tryParseOk)
                        listTypes.Add(enumValue);
                }
            }

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                ValueSearch = valueSearch,
                AssignmentStates = listTypes.Count != 0 ? listTypes : null,
                Sort = sort
            };

            var result = await _assignmentService.GetPagination(queryModel);

            return Ok(result);
        }

        [HttpGet("available-asset")]
        public async Task<IActionResult> GetAvailableAsset()
        {
            var result = await _assignmentService.CheckAvailableAsset();

            if (result == null)
                return Ok("Empty asset");

            return Ok(result);
        }

        [AuthorizeRoles(StaffRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AssignmentRequest requestModel)
        {
            var userId = _userProvider.GetUserId();

            if (id < 0)
            {
                return BadRequest("Invalid assignment");
            }
            var assignment = await _assignmentService.GetAssignmentById(id);
            if (assignment == null)
                return NotFound();

            if (assignment.State != AssignmentStateEnum.WaitingForAcceptance)
                return BadRequest("Invalid Assignment");

            requestModel.AssignedById = userId;

            var result = await _assignmentService.Update(id, requestModel);
            return Ok(result);
        }

        [HttpGet("getlistbyuserid")]
        public async Task<IActionResult> GetListByUserId()
        {
            var userId = _userProvider.GetUserId();
            if (userId == null)
                return NotFound();
            var user = await _userService.GetUserById(userId.Value);

            if (user == null) return StatusCode(500, "Sorry the request failed");

            var result = await _assignmentService.GetListByUserLoggedIn(userId.Value);

            if (result == null)
                return StatusCode(500, "Sorry the Request failed");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var assignment = await _assignmentService.GetAssignmentById(id);

            if (assignment == null)
                return StatusCode(500, "Can't found asset in the system");

            if (assignment.State != AssignmentStateEnum.WaitingForAcceptance && assignment.State != AssignmentStateEnum.Declined)
                return BadRequest("Invalid Assignment");

            await _assignmentService.Delete(id);

            return Ok(assignment);
        }
    }
}
