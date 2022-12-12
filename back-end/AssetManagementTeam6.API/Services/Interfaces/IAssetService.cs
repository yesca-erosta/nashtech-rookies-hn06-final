using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IAssetService
    {
        Task<Asset?> GetAssetByName(string model);
        Task<Asset>GetAssetById(int id);
        Task<Asset?> Create(AssetRequest createRequest);
        Task<Asset?> Update(int id, AssetRequest updateRequest);
        Task<bool> Delete(int id);
        Task<Pagination<GetAssetResponse?>> GetPagination(PaginationQueryModel queryModel, LocationEnum location);
    }
}
