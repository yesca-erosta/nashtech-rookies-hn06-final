using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class RequestForReturningRepository : BaseRepository<RequestForReturning>, IRequestForReturningRepository
    {
        public RequestForReturningRepository(AssetManagementContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<RequestForReturning>> GetListAsync(Expression<Func<RequestForReturning, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(request => request.Assignment)
                .Include(request => request.AcceptedBy)
                .Include(request => request.RequestedBy)
                .ToListAsync();
        }

        public override async Task<RequestForReturning?> GetOneAsync(Expression<Func<RequestForReturning, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(request => request.Assignment)
                .Include(request => request.AcceptedBy)
                .Include(request => request.RequestedBy)
                .FirstOrDefaultAsync();
        }
    }
}
