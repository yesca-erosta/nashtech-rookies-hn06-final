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
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly IUserService _userService;
        public AssetController(IAssetService assetService,IUserService userService)
        {
            _assetService = assetService;
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

            if (user == null) return NotFound();
            var location = user.Location;

            try
            {
                var entities = await _assetService.GetAllAsync(location);

                return new JsonResult(entities);
            }
            catch
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AssetRequest requestModel)
        {
            var asset = await _assetService.GetAssetByName(requestModel.AssetName);

            if (asset != null)
                return StatusCode(409, $"asset name {requestModel.AssetName} has already existed in the system");

            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();

            var user = await _userService.GetUserById(userId.Value);

            if (user == null) return StatusCode(500, "Sorry the request failed");

            requestModel.Location = user.Location;

            if (((int)requestModel.State) > 1) return BadRequest($"State invalid");

            var result = await _assetService.Create(requestModel);

            if (result == null)
                return StatusCode(500, "Sorry the Request failed");

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AssetRequest requestModel)
        {
            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();

            var asset = await _assetService.GetAssetById(requestModel.Id);
            if (asset == null)
                return NotFound();

            var user = await _userService.GetUserById((int)userId);

            requestModel.Location = user.Location;

            if ((int)requestModel.State == 4)
                    return BadRequest("State Invalid");
            
            var result = await _assetService.Update(requestModel);

            if (result == null)
                return StatusCode(500, "Sorry the Request failed");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var asset = await _assetService.GetAssetById(id);

            if (asset == null)
                return StatusCode(500, "Can't found asset in the system");

            await _assetService.Delete(id);

            return Ok(asset);
        }

        [HttpGet("query")]
        public async Task<IActionResult> Pagination(int page, int pageSize, string? valueSearch, string? sort,string? states,string? category)
        {
            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();

            var user = await _userService.GetUserById(userId.Value);

            var location = user!.Location;

            var listStates = new List<StateEnum>();

            if (!string.IsNullOrWhiteSpace(states)) 
               {
                var typeArr = states.Split(",");
                foreach (string typeValue in typeArr)
                {
                    var tryParseOk = (Enum.TryParse(typeValue, out StateEnum enumValue));
                    if (tryParseOk)
                        listStates.Add(enumValue);
                }
            }

            var listCategory = new List<string>();

            if (!string.IsNullOrWhiteSpace(category))
            {
                var typeArr = category.Split(",");
                foreach (string typeValue in typeArr)
                {
                    listCategory.Add(typeValue);
                }
            }

            var queryModel = new PaginationQueryModel
            {
                Page = page,
                PageSize = pageSize,
                StaffCodeOrName = valueSearch,
                State = listStates.Count != 0 ? listStates : null,
                Category = listCategory.Count != 0 ? listCategory : null,
                Sort = sort
            };

            var result = await _assetService.GetPagination(queryModel, location);

            return Ok(result);
        }
    }
}
