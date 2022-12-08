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
        private readonly IAssetService _assetService;
        private readonly IAssignmentService _assignmentService;
        private readonly IUserService _userService;
        private readonly IUserProvider _userProvider;
        public AssignmentController(IAssetService assetService,IAssignmentService assignmentService, IUserService userService, IUserProvider userProvider)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _userProvider = userProvider;
            _assetService = assetService;
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

            var asset =await _assetService.GetAssetById(requestModel.AssetId);

            if (asset.State != AssetStateEnum.Available)
                return BadRequest("Asset Not Available");

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
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> Pagination(int page, int pageSize, string? valueSearch, string? states,DateTime? date, string? sort)
        {
            var listStates = new List<AssignmentStateEnum>();

            if (!string.IsNullOrWhiteSpace(states))
            {
                var typeArr = states.Split(",");
                foreach (string typeValue in typeArr)
                {
                    var tryParseOk = (Enum.TryParse(typeValue, out AssignmentStateEnum enumValue));
                    if (tryParseOk)
                        listStates.Add(enumValue);
                }
            }

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                ValueSearch = valueSearch,
                AssignmentStates = listStates.Count != 0 ? listStates : null,
                Sort = sort,
                FilterByAssignedDates = date
            };

            var result = await _assignmentService.GetPagination(queryModel);

            return Ok(result);
        }

        [HttpGet("available-asset")]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> GetAvailableAsset()
        {
            var result = await _assignmentService.CheckAvailableAsset();

            if (result == null)
                return Ok("Empty asset");

            return Ok(result);
        }

        [HttpGet("getlistbyuserid")]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> GetListByUserId()
        {
            var userId = _userProvider.GetUserId();

            if (userId == null)
                return NotFound();

            var user = await _userService.GetUserById(userId.Value);

            if (user == null) return StatusCode(500, "Sorry the request failed");

            var result = await _assignmentService.GetListAssignmentByUserLoggedIn(userId.Value);

            if (result == null)
                return StatusCode(500, "Sorry the Request failed");

            return Ok(result);
        }

       
        [HttpPut("{id}")]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AssignmentRequest requestModel)
        {
            var userId = _userProvider.GetUserId();
            var location = _userProvider.GetLocation();

            if (userId == null && location == null) return StatusCode(500, "Sorry the request failed");

            var assignedTo = await _userService.GetUserById(requestModel.AssignedToId);

            if (assignedTo == null && assignedTo?.Location != location) return BadRequest("Assignee not same location");

            if (id < 0)
            {
                return BadRequest("Invalid Id");
            }

            var assignment = await _assignmentService.GetAssignmentById(id);
            if (assignment == null)
                return NotFound();

            if (assignment.State != AssignmentStateEnum.WaitingForAcceptance)
                return BadRequest("Invalid State");

            requestModel.AssignedById = userId;

            var result = await _assignmentService.Update(id, requestModel);

            return Ok(result);
        }

        [HttpPut("accepted/{id}")]
        [AuthorizeRoles(StaffRoles.Staff)]
        public async Task<IActionResult> AcceptedAssignment(int id)
        {
            var assignment = await _assignmentService.GetAssignmentById(id);

            if (assignment == null) return NotFound("This assignment has been removed");

            if (assignment.State != AssignmentStateEnum.WaitingForAcceptance) return BadRequest("This assignment is not waiting for acceptance");

            if (assignment.Asset.State != AssetStateEnum.Available) return BadRequest("Asset Not Available");

            var result = await _assignmentService.AcceptedAssignment(id);

            return Ok(result);
        }

        [HttpPut("declined/{id}")]
        [AuthorizeRoles(StaffRoles.Staff)]
        public async Task<IActionResult> DeclinedAssignment(int id)
        {
            var assignment = await _assignmentService.GetAssignmentById(id);

            if (assignment == null) return NotFound("This assignment has been removed");

            if (assignment.State != AssignmentStateEnum.WaitingForAcceptance) return BadRequest("This assignment is not waiting for acceptance");

            if (assignment.Asset.State != AssetStateEnum.Available) return BadRequest("Asset Not Available");

            var result = await _assignmentService.DeclinedAssignment(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var assignment = await _assignmentService.GetAssignmentById(id);

            if (assignment == null)
                return NotFound("This assignment has been removed");

            if (assignment.State != AssignmentStateEnum.WaitingForAcceptance && assignment.State != AssignmentStateEnum.Declined)
                return BadRequest("This assignment is not waiting for acceptance or Declined");

            var isDeleted = await _assignmentService.Delete(id);

            if (!isDeleted) return StatusCode(500, "Sorry the Request failed");

            return Ok(assignment);
        }
    }
}
