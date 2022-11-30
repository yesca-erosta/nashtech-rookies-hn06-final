using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    public class AssetRequest
    {
        public int? Id { get; set; }
        public string AssetName { get; set; }
        public string CategoryId { get; set; }
        public string Specification { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InstalledDate { get; set; }
        public StateEnum State { get; set; }    
        public LocationEnum Location { get; set; }
    }
}
