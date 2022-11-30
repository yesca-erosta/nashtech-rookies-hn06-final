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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet()]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();
            try
            {
                var data = await _categoryService.GetAllAsync();
                return new JsonResult(data);
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> CreateAsync(CategoryRequest requestModel)
        {
            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();
            var category = await _categoryService.GetCategoryById(requestModel.Id);

            if (requestModel.Id.Trim() == requestModel.Name.Trim())
                return BadRequest("Category is already existed. Please enter a different category");

            if (category != null)
                return BadRequest("CategoryID is already existed. Please enter a different CategoryID");
                
            var categoryNameCompare = await _categoryService.GetCategoryByName(requestModel.Name);

            if (categoryNameCompare != null)
                return BadRequest("Category is already existed. Please enter a different category");
            
            var data = await _categoryService.Create(requestModel);

            if (data == null)
                return StatusCode(500, "Sorry the Request failed");

            return Ok(data);
        }

        [HttpGet("{id}")]
        [AuthorizeRoles(StaffRoles.Admin)]
        public async Task<IActionResult> GetOneAsync(string id)
        {
            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return NotFound();

            var data = await _categoryService.GetCategoryById(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }
    }
}
