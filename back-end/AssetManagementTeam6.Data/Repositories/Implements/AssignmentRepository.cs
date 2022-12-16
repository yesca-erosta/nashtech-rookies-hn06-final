using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    [ExcludeFromCodeCoverage]
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(AssetManagementContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Assignment>> GetListAsync(Expression<Func<Assignment, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(a => a.Asset)
                .Include(a => a.AssignedBy)
                .Include(a => a.AssignedTo)
                .ToListAsync();
        }

        public override async Task<Assignment?> GetOneAsync(Expression<Func<Assignment, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(a => a.Asset)
                .Include(a => a.AssignedBy)
                .Include(a => a.AssignedTo)
                .FirstOrDefaultAsync();
        }
    }
}
