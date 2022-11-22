using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class Asset : BaseEntity<int>
    {
        public string? AssetName { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string? Specification { get; set; }
        [Required]
        public DateTime? InstalledDate { get; set; }

        public StateEnum State { get; set; }

        public string? AssetCode { get; private set; }
    }
}
