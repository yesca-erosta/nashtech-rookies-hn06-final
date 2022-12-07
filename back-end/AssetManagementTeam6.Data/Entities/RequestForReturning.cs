using Common.Enums;

namespace AssetManagementTeam6.Data.Entities
{
    public class RequestForReturning : BaseEntity<int>
    {
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int? RequestedById { get; set; }
        public User RequestedBy { get; set; }
        public int? AcceptedById { get; set; }
        public User? AcceptedBy { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public RequestForReturningStateEnum State { get; set; }
    }
}
