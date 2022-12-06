using Common.Enums;

namespace AssetManagementTeam6.API.Heplers
{
    public interface IUserProvider
    {
        int? GetUserId();
        LocationEnum? GetLocation();
    }
}
