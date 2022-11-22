using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    public class CreateUserRequest
    {
        //public string StaffCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinedDate { get; set; }
        public GenderEnum Gender { get; set; }
        public StaffEnum Type { get; set; }

    }
}
