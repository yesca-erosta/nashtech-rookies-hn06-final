using AssetManagementTeam6.Data.Entities;
using Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    [ExcludeFromCodeCoverage]
    public class UserRequest
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime JoinedDate { get; set; }
        public StaffEnum Type { get; set; }
        public LocationEnum Location { get; set; }
        public bool? NeedUpdatePwdOnLogin { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
