using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class Assignment : BaseEntity<int>
    {
        [Required]
        public string? AssignedTo { get; set; }
        [Required]
        public string? AssignedBy { get; set; }
        [Required]
        public DateTime? AssignedDate { get; set; }

        // TODO: type State
        [Required]
        public string? State { get; set; } 

        /*[Required]
        public string? AcceptedBy { get; set; }
        [Required]
        public string? ReturnDate { get; set; }
        [Required]
        public string? RequestedBy { get; set; }
        [Required]
        public string? AssetCode { get; set; }
        [Required]
        public string? AssetName { get; set; }
        [Required]
        public string? Specification { get; set; }*/
    }
}
