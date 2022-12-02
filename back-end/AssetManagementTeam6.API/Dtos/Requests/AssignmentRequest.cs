using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    [ExcludeFromCodeCoverage]
    public class AssignmentRequest
    {
        public int AssetId { get; set; }
        public int AssignedToId { get; set; }
        public int? AssignedById { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
    }
}
