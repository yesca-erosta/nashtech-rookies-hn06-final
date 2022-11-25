using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserResponse>> GetAllAsync(LocationEnum location);
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUserAccount(string userName);
        Task<User?> LoginUser(LoginRequest loginRequest);
        Task<User?> Create(UserRequest createRequest);
        Task<User?> ChangePassword(User updateRequest);
        Task<User?> Update(UserRequest updateRequest);
        Task<bool> Delete(int id);
        Task<Pagination<GetUserResponse?>> GetPagination(PaginationQueryModel queryModel, LocationEnum location);
    }
}
