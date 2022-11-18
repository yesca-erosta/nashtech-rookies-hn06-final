using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
