using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string UserName { get;  set; }
        public string StaffCode { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string FullName { get; set; }
        public DateTime? JoinedDate { get; set; }
        
    }
}
