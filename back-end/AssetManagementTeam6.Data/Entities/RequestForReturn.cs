using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class RequestForReturn : BaseEntity<int>
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public int RequestedById { get; set; }
        public User RequestedBy { get; set; }    
        public DateTime? AssignedDate { get; set; }   
        public int? AcceptedById { get; set; }
        public User? AcceptedBy { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public RequestForReturnStateEnum RequestForReturnState { get; set; }
        public LocationEnum Location { get; set; }
    }
}
