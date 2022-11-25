using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    public class GetAssetResponse
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string CategoryId { get; set; }     
        public string Specification { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string State { get; set; }
        public string AssetCode { get; set; }

        public GetAssetResponse(Asset asset)
        {
            Id = asset.Id;
            AssetName = asset.AssetName;
            CategoryId = asset.CategoryId;
            Specification = asset.Specification;
            InstalledDate = asset.InstalledDate;
            State = asset.State.ToString();
            AssetCode = asset.AssetCode;
        }
    }
}
