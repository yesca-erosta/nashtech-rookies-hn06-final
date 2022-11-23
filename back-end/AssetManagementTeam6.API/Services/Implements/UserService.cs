using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;

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

        public async Task<bool> Delete(int id)
        {
            var deleteUser = await _userRepository.GetOneAsync(user => user.Id == id);

            if(deleteUser == null)
            {
                return false;
            }

            await _userRepository.Delete(deleteUser);

            return true;
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
