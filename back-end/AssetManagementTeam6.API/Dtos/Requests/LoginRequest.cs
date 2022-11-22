using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
