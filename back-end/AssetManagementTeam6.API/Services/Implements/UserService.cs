using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
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

        public async Task<User?> Create(UserRequest user)
        {
            user.NeedUpdatePwdOnLogin = true;

            var newUser = new User
            {
                UserName = user.UserName,
                Password = user.Password,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                Location = user.Location,
                JoinedDate = user.JoinedDate,
                Type = user.Type,
                NeedUpdatePwdOnLogin = (bool)user.NeedUpdatePwdOnLogin
            };

            newUser.Password = SystemFunction.CreateMD5(user.Password);

            var createdUser = await _userRepository.Create(newUser);

            return createdUser;
        }

        public async Task<IEnumerable<GetUserResponse>> GetAllAsync(LocationEnum location)
        {
            var users = await _userRepository.GetListAsync();

            var userByLocation = users.Where(x => x.Location == location);

            return userByLocation.Select(user => new GetUserResponse(user)
            {
                StaffCode = user.StaffCode!,
                FullName = user.FullName!,
                UserName = user.UserName,
                JoinedDate = user.JoinedDate,
                Type = user.Type.ToString(),
                Location = user.Location.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            }).ToList();
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

            loginRequest.Password = SystemFunction.CreateMD5(loginRequest.Password);

            return await _userRepository
               .GetOneAsync(user => user.UserName == loginRequest.UserName &&
                                   user.Password == loginRequest.Password);
        }

        public async Task<bool> Delete(int id)
        {
            var deleteUser = await _userRepository.GetOneAsync(user => user.Id == id);

            if (deleteUser == null)
            {
                return false;
            }

            await _userRepository.Delete(deleteUser);

            return true;
        }

        //sort option, nametoquery, filterbyname, type

        public async Task<Pagination<GetUserResponse?>> GetPagination(PaginationQueryModel queryModel, LocationEnum location)
        {
            // TODO: get list users not set with location
            var users = await _userRepository.GetListAsync(x => x.Location == location);

            // filter by type
            if (queryModel.Types != null)
            {
                users = users?.Where(u => queryModel.Types.Contains(u.Type))?.ToList();
            }

            // search user by staffcode or fullname
            var nameToQuery = "";
            if (!string.IsNullOrEmpty(queryModel.StaffCodeOrName))
            {
                nameToQuery = queryModel.StaffCodeOrName.Trim().ToLower();
                users = users?.Where(u => u!.UserName!.ToLower().Contains(nameToQuery) ||
                                        u!.StaffCode!.ToLower().Contains(nameToQuery))?.ToList();
            }

            //sorting
            var sortOption = queryModel.Sort ??= Constants.NameAcsending;

            switch (sortOption)
            {
                case Constants.NameAcsending:
                    users = users?.OrderBy(u => u.FullName)?.ToList();
                    break;
                case Constants.NameDescending:
                    users = users?.OrderByDescending(u => u.FullName)?.ToList();
                    break;
                case Constants.StaffCodeAcsending:
                    users = users?.OrderBy(u => u.StaffCode)?.ToList();
                    break;
                case Constants.StaffCodeDescending:
                    users = users?.OrderByDescending(u => u.StaffCode)?.ToList();
                    break;
                case Constants.JoinedDateAcsending:
                    users = users?.OrderBy(u => u.JoinedDate)?.ToList();
                    break;
                case Constants.JoinedDateDescending:
                    users = users?.OrderByDescending(u => u.JoinedDate)?.ToList();
                    break;
                case Constants.TypeAcsending:
                    users = users?.OrderBy(u => u.Type)?.ToList();
                    break;
                case Constants.TypeDescending:
                    users = users?.OrderByDescending(u => u.Type)?.ToList();
                    break;
                default:
                    users = users?.OrderBy(u => u.FullName)?.ToList();
                    break;              
            }

            //paging
            if (users == null || users.Count() == 0)
            {
                return new Pagination<GetUserResponse?>
                {
                    Source = null,
                    TotalPage = 1,
                    TotalRecord = 0,
                    QueryModel = queryModel
                };
            }

            var output = new Pagination<GetUserResponse>();

            output.TotalRecord = users.Count();

            var listUsers = users.Select(user => new GetUserResponse(user));

            output.Source = listUsers.Skip((queryModel.Page - 1) * queryModel.PageSize)
                                    .Take(queryModel.PageSize)
                                    .ToList();

            output.TotalPage = (output.TotalRecord - 1) / queryModel.PageSize + 1;

            if (queryModel.Page > output.TotalPage)
            {
                queryModel.Page = output.TotalPage;
            }

            output.QueryModel = queryModel;

            return output;
        }

        public async Task<User?> ChangePassword(User updateRequest)
        {

            return await _userRepository.Update(updateRequest);
        }

        public async Task<User?> Update(int id, UserRequest updateRequest)
        {

            var userUpdate = await _userRepository.GetOneAsync(user => user.Id == id);

            if (userUpdate == null) 
            {
                return null;
            }
            userUpdate!.DateOfBirth = updateRequest.DateOfBirth;
            userUpdate.Gender = updateRequest.Gender;
            userUpdate.JoinedDate = updateRequest.JoinedDate;
            userUpdate.Type = updateRequest.Type;

            var updatedUser = await _userRepository.Update(userUpdate);

            return updatedUser;
        }

        public async Task<User?> GetUserByStaffCode(string staffcode)
        {
            return await _userRepository.GetOneAsync(user => user!.StaffCode == staffcode);
        }
    }
}
