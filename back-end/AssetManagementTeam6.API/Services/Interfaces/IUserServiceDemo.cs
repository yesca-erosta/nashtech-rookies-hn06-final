using AssetManagementTeam6.API.Dtos.Models;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IUserServiceDemo
    {
        Task<UserModel?> GetUserById(int id);
        Task<LoginResponse?> LoginUser(LoginRequest loginRequest);
    }
}
