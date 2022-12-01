using AssetManagementTeam6.API.Attributes;
using AssetManagementTeam6.API.Dtos.Requests;
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
        public AssignmentController(IAssignmentService assignmentService, IUserService userService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
        }

        [HttpPost]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> CreateAsync([FromBody] AssignmentRequest requestModel)
        {
            var userId = this.GetCurrentLoginUserId();

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
    }
}
