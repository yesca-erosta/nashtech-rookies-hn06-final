using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    public class AssignmentRequest
    {
        public int AssetId { get; set; }
        //public Asset Asset { get; set; }
        public int AssignedToID { get; set; }
       // public User AssignedTo { get; set; }
        public int? AssignedById { get; set; }
        //public User? AssignedBy { get; set; }
        public DateTime AssignedDate { get; set; }
        public AssignmentStateEnum State { get; set; }
        public string? Note { get; set; }
    }
}
