using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string StaffCode { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? JoinedDate { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public GenderEnum Gender { get; set; }
        public GetUserResponse(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            StaffCode = user.StaffCode;
            Type = user.Type.ToString();
            Location = user.Location.ToString();
            FullName = user.FullName;
            Gender = user.Gender;
            JoinedDate = user.JoinedDate;
            FirstName = user.FirstName;
            LastName = user.LastName;
            DateOfBirth = user.DateOfBirth;
        }
    }
}
