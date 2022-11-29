using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementTeam6.Data.Entities
{
    public class Asset : BaseEntity<int>
    {
        public string? AssetName { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string? Specification { get; set; }
        public DateTime? InstalledDate { get; set; }

        public StateEnum State { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? AssetCode { get; set; }
        public LocationEnum Location { get; set; }

    }
}
