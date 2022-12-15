using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementTeam6.Data.Entities
{
    public class User : BaseEntity<int>
    {
        private string _fullName;

        private string _staffcode;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime JoinedDate { get; set; }
        public StaffEnum Type { get; set; }
        public LocationEnum Location { get; set; }
        public bool NeedUpdatePwdOnLogin { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? FullName
        {
            get { return _fullName; }
            set { _fullName = $"{FirstName} {LastName}"; }
        } 
        public string UserName { get; set; }

        public string Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? StaffCode
        {
            get { return _staffcode; }
            set { _staffcode = $"SD{Id.ToString("D4")}"; }
        }

    }
}
