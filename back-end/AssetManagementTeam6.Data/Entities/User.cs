using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class User : BaseEntity<int>
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime JoinedDate { get; set; }
        public StaffEnum Type { get; set; }

        public LocationEnum Location { get; set; }
        public bool IsFirst { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public string Username
        {
            get
            {
                var splitName = LastName.ToLower().Split(" ");
                var lastName = "";
                for (int i = 0; i < splitName.Count(); i++)
                {
                    lastName += splitName[i][0];
                }
                return FirstName.ToLower() + lastName;
            }
        }
        // TODO: type hash password
        public string Password { get => $"{Username}@{DateOfBirth.ToString("ddMMyyyy")}"; }
        public string StaffCode { get => $"SD{Id.ToString("D4")}"; }
    }
}
