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
    public class RequestForReturningController : ControllerBase
    {
        private readonly IRequestForReturningService _requestForReturningService;
        private readonly IUserProvider _userProvider;
        private readonly IUserService _userService;
        public RequestForReturningController(IRequestForReturningService requestForReturningService, IUserProvider userProvider, IUserService userService )
        {
            _requestForReturningService = requestForReturningService;
            _userProvider = userProvider;
            _userService = userService;
        }

        [HttpPost]
        [AuthorizeRoles(StaffRoles.Admin,StaffRoles.Staff)]
        public async Task<IActionResult> CreateAsync([FromBody] RequestForReturningRequest requestModel)
        {
            try
            {
                var userId = _userProvider.GetUserId();

                if (userId == null)
                    return NotFound();

                var user = await _userService.GetUserById(userId.Value);

                if (user == null) return StatusCode(500, "Wrong user");

                requestModel.RequestedById = userId;

                var result = await _requestForReturningService.Create(requestModel);

                if (result == null)
                    return StatusCode(500, "Sorry the Request failed");

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet()]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _requestForReturningService.GetAllAsync();

            if(result == null)
            {
                return StatusCode(500, "Sorry the Request failed");
            }

            return Ok(result);
        }

        [HttpGet("query")]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> Pagination(int page, int pageSize, string? valueSearch, string? states, DateTime? date, string? sort)
        {
            var listStates = new List<RequestForReturningStateEnum>();

            if (!string.IsNullOrWhiteSpace(states))
            {
                var typeArr = states.Split(",");
                foreach (string typeValue in typeArr)
                {
                    var tryParseOk = (Enum.TryParse(typeValue, out RequestForReturningStateEnum enumValue));
                    if (tryParseOk)
                        listStates.Add(enumValue);
                }
            }

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                ValueSearch = valueSearch,
                RequestForReturningState = listStates.Count != 0 ? listStates : null,
                FilterByReturnedDates = date,
                Sort = sort
            };

            var result = await _requestForReturningService.GetPagination(queryModel);

            return Ok(result);
        }
    }
}
