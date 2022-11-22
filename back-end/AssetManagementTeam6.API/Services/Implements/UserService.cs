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

        public async Task<CreateUserResponse> Create(CreateUserRequest createRequest)
        {
            var user = new User
            {
                IsFirst = true,
                FirstName = createRequest.FirstName,
                LastName = createRequest.LastName,
                DateOfBirth = createRequest.DateOfBirth,
                JoinedDate = createRequest.JoinedDate,
                Gender = createRequest.Gender,
                Type = createRequest.Type,
                Location = LocationEnum.HN
            };

            var createdUser = await _userRepository.Create(user);

            return new CreateUserResponse(createdUser);
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
