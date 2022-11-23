using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUserAccount(string userName);
        Task<User?> LoginUser(LoginRequest loginRequest);
        Task<User?> Create(User createRequest);
        Task<User?> Update(User updateRequest);
        Task<bool> Delete(int id);
    }
}
