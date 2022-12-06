using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IRequestForReturnService
    {
        Task<RequestForReturn?> Create(RequestforReturnRequest createRequest,int assignmentState);
        Task<RequestForReturn> GetRequestForReturnByRequestedUser(int requestedUserId);
        Task<RequestForReturn> GetRequestForReturnByAsset(int assetId);
        Task<Pagination<RequestForReturn?>> GetPagination(PaginationQueryModel queryModel, LocationEnum location);
    }
}
