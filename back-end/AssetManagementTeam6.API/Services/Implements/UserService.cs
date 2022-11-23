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

        public async Task<User?> Create(User user)
        {
            user.NeedUpdatePwdOnLogin = true;

            var createdUser = await _userRepository.Create(user);

            return createdUser;
        }

        public async Task<IEnumerable<GetUserResponse>> GetAllAsync(LocationEnum location)
        {
            var users = await _userRepository.GetListAsync();

            var userByLocation = users.Where(x => x.Location == location);

            return userByLocation.Select(user => new GetUserResponse
            {
                Id = user.Id,
                StaffCode = user.StaffCode,
                FullName = user.FullName,
                UserName = user.UserName,
                JoinedDate = user.JoinedDate,
                Type = user.Type.ToString(),
                Location = user.Location.ToString(),
            });
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetOneAsync(user => user.Id == id);
        }

        public async Task<User?> GetUserByUserAccount(string userName)
        {
            return await _userRepository.GetOneAsync(user => user.UserName == userName);
        }

        public async Task<User?> LoginUser(LoginRequest loginRequest)
        {
            return await _userRepository
               .GetOneAsync(user => user.UserName == loginRequest.UserName &&
                                   user.Password == loginRequest.Password);
        }

        public async Task<User?> Update(User updateRequest)
        {
            return await _userRepository.Update(updateRequest);
        }
    }
}
