using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class User : BaseEntity<int>
    {
        [Required]
        public string StaffCode { get; set; }
        [Required]
        public string Username { get; set; }

        // TODO: type hash password
        [Required]
        public string Password { get; set; }
     
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
       
        public DateTime? DateOfBirth { get; set; }
       
        public GenderEnum? Gender { get; set; }
        public DateTime? JoinedDate { get; set; }

        [Required]
        public StaffEnum Type { get; set; }

        [Required]
        public LocationEnum Location { get; set; }

        [Required]
        public bool IsFirst { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
