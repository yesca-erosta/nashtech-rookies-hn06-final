using Common.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace AssetManagementTeam6.API.Heplers
{
    [ExcludeFromCodeCoverage]
    public class UserProvider : IUserProvider
    {
        private readonly ClaimsIdentity? _identity;

        public UserProvider(IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.HttpContext?.User?.Identity;
            if (identity == null)
            {
                _identity = null;
            }
            else
            {
                _identity = identity as ClaimsIdentity;
            }
        }

        public LocationEnum? GetLocation()
        {
            
            var locationIntStr = _identity?.FindFirst("Location")?.Value;

            if (Enum.TryParse(locationIntStr, out LocationEnum enumValue))
            {
                return enumValue;
            }

            return null;
        }

        public int? GetUserId()
        {
            var userIdString = _identity?.FindFirst("UserId")?.Value;

            if (string.IsNullOrWhiteSpace(userIdString))
                return null;

            var isUserIdValid = int.TryParse(userIdString, out int userId);

            if (!isUserIdValid)
                return null;

            return userId;

        }
    }
}
