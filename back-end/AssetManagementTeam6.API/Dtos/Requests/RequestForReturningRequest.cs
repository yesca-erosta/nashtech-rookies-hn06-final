using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    public class RequestForReturningRequest
    {
        public int AssignmentId { get; set; }
        public int? RequestedById { get; set; }
        public int? AcceptedById { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public RequestForReturningStateEnum? State { get; set; }
    }
}
