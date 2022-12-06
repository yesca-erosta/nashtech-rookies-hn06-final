using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace AssetManagementTeam6.API.Test.Services.ToRemove
{
    [ExcludeFromCodeCoverage]
    public class FakeUserRepository : IUserRepository
    {
        const string jsonData = "dummy_user_data.json";

        public Task<User> Create(User entity)
        {
            return Task.FromResult(entity);
        }

        public Task<bool> Delete(User? entity)
        {
            var output = false;
            if (entity != null)
            {
                var listEntity = TestBase.ReadJsonFromFile<List<User>>(jsonData);
                var user = listEntity!.FirstOrDefault(x => x.Id == entity.Id);
                output = user != null;
            }

            return Task.FromResult(output);
        }

        public Task<IEnumerable<User>> GetListAsync(Expression<Func<User, bool>>? predicate = null)
        {
            var listEntity = TestBase.ReadJsonFromFile<List<User>>(jsonData);
            return Task.FromResult((IEnumerable<User>)listEntity);
        }

        public Task<User?> GetOneAsync(Expression<Func<User, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
