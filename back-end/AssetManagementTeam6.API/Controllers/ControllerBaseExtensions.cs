using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssetManagementTeam6.API.Controllers
{
    public static class ControllerBaseExtensions
    {
        public static int? GetCurrentLoginUserId(this ControllerBase controller)
        {
            var abc = controller.HttpContext;
            if (controller.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userIdString = identity?.FindFirst("UserId")?.Value;

                if (string.IsNullOrWhiteSpace(userIdString))
                    return null;

                var isUserIdValid = int.TryParse(userIdString, out int userId);

                if (!isUserIdValid)
                    return null;

                return userId;
            }
            else
            {
                return null;
            }
        }
    }
}
