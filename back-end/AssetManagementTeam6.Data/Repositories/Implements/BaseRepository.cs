﻿using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace AssetManagementTeam6.Data.Repositories.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        // implement interface User 

        protected readonly DbSet<T> _dbSet;
        protected readonly AssetManagementContext _context;
        public BaseRepository(AssetManagementContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public async Task<T> Create(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("null");
            }

            await _context.Database.BeginTransactionAsync();
            
            try
            {
                await _dbSet.AddAsync(entity);

                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync();
            }
            catch (System.Exception)
            {
                await _context.Database.RollbackTransactionAsync();
            }

            return entity;
        }

        public async Task<bool> Delete(T? entity)
        {
            if(entity == null)
            {
                throw new ArgumentException("entity");
            }

            await _context.Database.BeginTransactionAsync();

            try {
                _dbSet.Remove(entity);

                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();

                return false;
            }               
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
        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await _context.Database.BeginTransactionAsync();

            try
            {
                _dbSet.Update(entity);

                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync();
            }
            catch (System.Exception)
            {
                await _context.Database.RollbackTransactionAsync();

            }

            return entity;
        }

    }
}
