using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Attributes
{
    [ExcludeFromCodeCoverage]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
