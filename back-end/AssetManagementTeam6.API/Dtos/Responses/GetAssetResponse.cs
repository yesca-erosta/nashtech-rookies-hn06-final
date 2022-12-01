using AssetManagementTeam6.Data.Entities;
using Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    [ExcludeFromCodeCoverage]
    public class GetAssetResponse
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string CategoryId { get; set; }     
        public Category Category { get; set; }
        public string Specification { get; set; }
        public DateTime? InstalledDate { get; set; }
        public StateEnum State { get; set; }
        public string AssetCode { get; set; }

        public GetAssetResponse(Asset asset)
        {
            Category = asset.Category;
            Id = asset.Id;
            AssetName = asset.AssetName!;
            CategoryId = asset.CategoryId;
            Specification = asset.Specification!;
            InstalledDate = asset.InstalledDate;
            State = asset.State;
            AssetCode = asset.AssetCode!;
        }
    }
}
