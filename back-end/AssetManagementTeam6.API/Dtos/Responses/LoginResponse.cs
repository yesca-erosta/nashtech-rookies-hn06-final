using Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    [ExcludeFromCodeCoverage]
    public class LoginResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool NeedUpdatePwdOnLogin { get; set; }
        public StaffEnum Type { get; set; }
        public LocationEnum Location { get; set; }
        public string? Token { get; set; }
    }
}
