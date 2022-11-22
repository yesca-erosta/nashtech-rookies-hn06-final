using AssetManagementTeam6.API.Dtos.Models;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(User user)
        {
            user.NeedUpdatePwdOnLogin = true;

            var createdUser = await _userRepository.Create(user);

            return createdUser;
        }

        public async Task<UserModel?> GetUserById(int id)
        {
            var user = await _userRepository.GetOneAsync(user => user.Id == id);

            if (user != null)
            {
                return new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            return null;
        }

        public async Task<LoginResponse?> LoginUser(LoginRequest loginRequest)
        {
            var user = await _userRepository
               .GetOneAsync(user => user.UserName == loginRequest.UserName &&
                                   user.Password == loginRequest.Password);

            if (user == null)
            {
                return null;
            }    

            return new LoginResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Type = user.Type.ToString()
            };
        }

    }
}
