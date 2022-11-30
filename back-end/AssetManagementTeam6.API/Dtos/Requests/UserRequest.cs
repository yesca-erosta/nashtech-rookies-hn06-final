using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Requests
{
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


        //public UserRequest(User user)
        //{
        //    Id = user.Id;
        //    FirstName = user.FirstName;
        //    LastName = user.LastName;
        //    DateOfBirth = user.DateOfBirth;
        //    Gender = user.Gender;
        //    JoinedDate = user.JoinedDate;
        //    Type = user.Type;
        //    Location = user.Location;
        //    NeedUpdatePwdOnLogin = user.NeedUpdatePwdOnLogin;
        //    UserName = user.UserName;
        //    Password = user.Password;
        //}
    }
}
