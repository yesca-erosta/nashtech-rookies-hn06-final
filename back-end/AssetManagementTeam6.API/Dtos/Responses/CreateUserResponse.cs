using AssetManagementTeam6.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    [ExcludeFromCodeCoverage]
    public class CreateUserResponse
    {
        public string UserName { get;  set; }
        public string StaffCode { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string PassWord { get; set; }

        public CreateUserResponse (User user)
        {
            StaffCode = user.StaffCode!;
            UserName = user.UserName;
            Type = user.Type.ToString();
            Location = user.Location.ToString();
            PassWord = user.Password;
        }
    }
}
