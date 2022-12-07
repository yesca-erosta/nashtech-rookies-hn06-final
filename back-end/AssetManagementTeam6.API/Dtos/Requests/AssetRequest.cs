using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Requests
{
    [ExcludeFromCodeCoverage]
    public class AssetRequest
    {
        public int? Id { get; set; }
        public string AssetName { get; set; }
        public string CategoryId { get; set; }
        public string Specification { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InstalledDate { get; set; }
        public AssetStateEnum State { get; set; }    
        public LocationEnum Location { get; set; }
    }
}
