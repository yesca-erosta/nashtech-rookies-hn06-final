using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<Assignment?> Create(AssignmentRequest createRequest);
        Task<Assignment> GetAssignmentByAssignedUser(int assignedUserId);
        Task<Assignment> GetAssignmentByAssignedAsset(int assetId);
        Task<IEnumerable<GetAssignmentResponse>> GetAllAsync();
        Task<Pagination<GetAssignmentResponse?>> GetPagination(PaginationQueryModel queryModel);
        Task<IEnumerable<GetAssetResponse>> CheckAvailableAsset();
        Task<IEnumerable<GetAssignmentResponse>> GetListByUserLoggedIn(int id);
    }
}
