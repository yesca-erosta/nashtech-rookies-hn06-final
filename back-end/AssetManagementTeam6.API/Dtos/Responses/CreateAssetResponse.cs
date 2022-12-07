using AssetManagementTeam6.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    [ExcludeFromCodeCoverage]
    public class CreateAssetResponse
    {
        public string AssetName { get;  set; }
        public string CategoryId { get; set; }
        public string Specification { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string State { get; set; }

        public CreateAssetResponse (Asset asset)
        {
            AssetName = asset.AssetCode!;
            CategoryId = asset.CategoryId;
            Specification = asset.Specification!;
            InstalledDate = asset.InstalledDate;
            State = asset.State.ToString();
        }
    }
}
