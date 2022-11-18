using AssetManagementTeam6.API.Dtos.Models;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Repositories.Interfaces;

namespace AssetManagementTeam6.API.Services.Implements
{
    public class UserServiceDemo : IUserServiceDemo
    {
        private readonly IUserRepository _userRepository;

        public UserServiceDemo(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<UserModel?> GetUserById(int id)
        {
            var user = await _userRepository.GetOneAsync(user => user.Id == id);

            if (user != null)
            {
                return new UserModel
                {
                    Id = user.Id,
                    UserName = user.Username
                };
            }

            return null;
        }

        public async Task<LoginResponse?> LoginUser(LoginRequest loginRequest)
        {
            var user = await _userRepository
               .GetOneAsync(user => user.Username == loginRequest.UserName &&
                                   user.Password == loginRequest.Password);

            if (user == null)
            {
                return null;
            }    

            return new LoginResponse
            {
                Id = user.Id,
                UserName = user.Username,
                Type = user.Type.ToString()
            };
        }

    }
}
