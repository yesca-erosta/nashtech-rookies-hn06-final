using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<Assignment?> Create(AssignmentRequest createRequest);
        Task<Assignment> GetAssignmentByAssignedUser(int assignedUserId);
        Task<Assignment> GetAssignmentByAssignedAsset(int assetId);
        Task<IEnumerable<GetAssignmentResponse>> GetAllAsync();
        Task<Pagination<GetAssignmentResponse?>> GetPagination(PaginationQueryModel queryModel);
        Task<GetAssignmentResponse> Update(int id,AssignmentRequest updateRequest);
        Task<Assignment?> GetAssignmentById(int id);
        Task<bool> Delete(int id);
        Task<IEnumerable<GetAssetResponse>> GetAllAssignedAsset();
        Task<IEnumerable<GetUserResponse>> GetAllAssignedUser();
        Task<IEnumerable<GetAssetResponse>> CheckAvailableAsset();
        Task<IEnumerable<GetAssignmentResponse>> GetListByUserLoggedIn(int id);
        Task<GetAssignmentResponse> ChangeStateAssignment(int id,AssignmentStateEnum updateRequest);

    }
}
