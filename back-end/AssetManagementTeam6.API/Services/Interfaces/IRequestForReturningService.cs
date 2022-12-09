using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IRequestForReturningService
    {
        Task<RequestForReturning?> Create(RequestForReturningRequest createRequest);
        Task<IEnumerable<GetRequestForReturningResponse>> GetAllAsync();
        Task<Pagination<GetRequestForReturningResponse?>> GetPagination(PaginationQueryModel queryModel);
        Task<GetRequestForReturningResponse> CompleteReturningRequest(RequestForReturning request);
        Task<GetRequestForReturningResponse> CancelReturningRequest(RequestForReturning request);
        Task<RequestForReturning> GetRequestForReturningById(int id);
    }
}
