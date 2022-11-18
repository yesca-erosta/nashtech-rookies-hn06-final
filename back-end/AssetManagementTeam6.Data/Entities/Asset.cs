using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class Asset : BaseEntity<int>
    {
        [Required]
        public string? AssetName { get; set; }

        // TODO: type Category
        [Required]
        public Category? Category { get; set; } 
        [Required]
        public string? Specification { get; set; }
        [Required]
        public DateTime? InstalledDate { get; set; }

        // TODO: type State
        [Required]
        public string? State { get; set; }
    }
}
