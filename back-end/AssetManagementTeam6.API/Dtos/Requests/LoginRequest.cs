
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    [ExcludeFromCodeCoverage]
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
