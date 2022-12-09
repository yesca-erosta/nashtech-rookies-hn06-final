using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class Report : BaseEntity<int>
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public int Total { get; set; }
        public int? AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
