using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity<int>
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly AssetManagementContext _context;
        public BaseRepository(AssetManagementContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public T Create(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public bool Delete(T? entity)
        {
            _dbSet.Remove(entity);

            return true;
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet.FirstOrDefaultAsync() : _dbSet.FirstOrDefaultAsync(predicate);

            return await dbSet;
        }
        public T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }

    }
}
