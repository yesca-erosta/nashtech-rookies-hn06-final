using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    [ExcludeFromCodeCoverage]
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(AssetManagementContext context) : base(context)
        {

        }
        public override async Task<IEnumerable<Asset>> GetListAsync(Expression<Func<Asset, bool>>? predicate = null)
        {
            var dbSet = predicate == null ?  _dbSet :  _dbSet.Where(predicate);

            return await dbSet.Include(asset => asset.Category).ToListAsync();
        }
        public override async Task<Asset?> GetOneAsync(Expression<Func<Asset, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
            .Include(asset => asset.Category)
            .FirstOrDefaultAsync();
        }
    }
}
