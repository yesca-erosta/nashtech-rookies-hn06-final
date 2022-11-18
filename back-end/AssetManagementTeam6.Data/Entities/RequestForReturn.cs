using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class RequestForReturn : BaseEntity<int>
    {
        [Required]
        public string? RequestedBy { get; set; }
        [Required]
        public DateTime? AssignedDate { get; set; }
        [Required]
        public string? AcceptedBy { get; set; }
        [Required]
        public DateTime? ReturnedDate { get; set; }

        // TODO: type State
        [Required]
        public string? State { get; set; }
    }
}
