using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    public class AssetRequest
    {
        public string AssetName { get; set; }
        public string CategoryId { get; set; }
        public string Specification { get; set; }
        public DateTime? InstalledDate { get; set; }
        public StateEnum State { get; set; }    
        public LocationEnum Location { get; set; }
    }
}
