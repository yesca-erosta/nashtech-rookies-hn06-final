using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManagementTeam6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest requestModel)
        {
            var user = await _userService.LoginUser(requestModel);

            if (user == null)
                return BadRequest("Username or password is incorrect!");

            var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Role, user.Type.ToString()),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("UserName", user.UserName)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.Key));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expired = DateTime.UtcNow.AddMinutes(JwtConstant.ExpiredTime);

            var token = new JwtSecurityToken(JwtConstant.Issuer,
                                            JwtConstant.Audience,
                                            claims, expires: expired,
                                            signingCredentials: signIn);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Type = user.Type,
                Location = user.Location,
                NeedUpdatePwdOnLogin = user.NeedUpdatePwdOnLogin,
                Token = tokenString
            });
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest requestModel)
        {

            var userId = this.GetCurrentLoginUserId();

            if (userId == null)
                return BadRequest("Password is incorrect!");

            var user = await _userService.GetUserById(userId.Value);
                
            if (user == null)
                return BadRequest("Password is incorrect!");

            if (user.Password != requestModel.OldPassword)
                return BadRequest("Password is incorrect!");

            if (user.Password == requestModel.NewPassword)
                return BadRequest("New password must not the same with the old password");

            user.Password = requestModel.NewPassword;
            user.NeedUpdatePwdOnLogin = false;

            user = await _userService.Update(user!);

            if (user == null)
                return StatusCode(500, "Sorry the Request failed");

            return Ok();
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserById(int id)
        //{
        //    try
        //    {
        //        var data = await _userService.GetUserById(id);

        //        return data != null ? Ok(data) : NotFound();
        //    }
        //    catch
        //    {
        //        return BadRequest("Bad request");
        //    }
        //}

        //[HttpGet("check")]
        //public async Task<IActionResult> CheckLogin()
        //{
        //    var userId = this.GetCurrentLoginUserId();

        //    if (userId == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(userId);
        //}

        //[HttpGet("logout")]
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    return Ok("Logout");
        //}
    }
}
