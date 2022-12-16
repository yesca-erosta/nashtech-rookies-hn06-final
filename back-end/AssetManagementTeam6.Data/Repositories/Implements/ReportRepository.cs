using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    [ExcludeFromCodeCoverage]
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(AssetManagementContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Report>> GetListAsync(Expression<Func<Report, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                        .Include(r => r.Category)
                        .ToListAsync();
        }

        public override async Task<Report?> GetOneAsync(Expression<Func<Report, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                            .Include(r => r.Category)
                            .FirstOrDefaultAsync();
        }
    }
}
