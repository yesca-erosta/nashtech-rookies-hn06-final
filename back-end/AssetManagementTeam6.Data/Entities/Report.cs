using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class Report : BaseEntity<int>
    {
        // TODO: type Category
        [Required]
        public Category? Category { get; set; } 
        [Required]
        public int? Total { get; set; }
        [Required]
        public int? Assigned { get; set; }
        [Required]
        public int? Available { get; set; }
        [Required]
        public int? NotAvailable { get; set; }
        [Required]
        public int? WaitingForRecycling { get; set; }
        [Required]
        public int? Recycled { get; set; }
    }
}
