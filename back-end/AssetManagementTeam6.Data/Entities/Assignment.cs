using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementTeam6.Data.Entities
{
    public class Assignment : BaseEntity<int>
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public int AssignedToID { get; set; }
        public User AssignedTo { get; set; }
        public int? AssignedById { get; set; }
        public User? AssignedBy { get; set; }
        public DateTime AssignedDate { get; set; }
        public AssignmentStateEnum State { get; set; }
        public string? Note { get; set; }
    

    }
}
