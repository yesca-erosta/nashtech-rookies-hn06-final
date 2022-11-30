using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    public class AssignmentRequest
    {
        public int AssetId { get; set; }
        public int AssignedToID { get; set; }
        public int? AssignedById { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
    }
}
