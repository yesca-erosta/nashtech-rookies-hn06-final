using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class RequestForReturnRepository : BaseRepository<RequestForReturn>, IRequestForReturnRepository
    {
        public RequestForReturnRepository(AssetManagementContext context) : base(context)
        {

        }
        public override async Task<IEnumerable<RequestForReturn>> GetListAsync(Expression<Func<RequestForReturn, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(a => a.Asset)
                .Include(a => a.RequestedBy)
                .Include(a => a.AcceptedBy)
                .ToListAsync();
        }

        public override async Task<RequestForReturn?> GetOneAsync(Expression<Func<RequestForReturn, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(a => a.Asset)
                .Include(a => a.RequestedBy)
                .Include(a => a.AcceptedBy)
                .FirstOrDefaultAsync();
        }
    }
}
