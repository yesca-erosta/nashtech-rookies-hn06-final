using AssetManagementTeam6.API.Dtos.Models;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel?> GetUserById(int id);
        Task<LoginResponse?> LoginUser(LoginRequest loginRequest);
       // Task DeleteAll(int id);
       Task<CreateUserResponse> Create(CreateUserRequest createRequest);
    }
}
