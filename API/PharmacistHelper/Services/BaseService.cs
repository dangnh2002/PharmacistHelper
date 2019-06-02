using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PharmacistHelper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PharmacistHelper.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly DbContext _dbContext;
        public BaseService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }
        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }
        public void InsertIfNotExist(Expression<Func<T, Guid>> identifierExpression, List<T> entities)
        {
            foreach (var entity in entities)
            {
                var v = identifierExpression.Compile()(entity);
                var predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(identifierExpression.Body, Expression.Constant(v)), identifierExpression.Parameters);

                var entry = Get(predicate);
                if (entry == null)
                {
                    Insert(entity);
                }
            }
        }
        public T Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
            return entity;
        }
        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                    bool isDisableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (isDisableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }
        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task InsertIfNotExistAsync(Expression<Func<T, Guid>> identifierExpression, List<T> entities)
        {
            foreach (var entity in entities)
            {
                var v = identifierExpression.Compile()(entity);
                var predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(identifierExpression.Body, Expression.Constant(v)), identifierExpression.Parameters);

                var entry = Get(predicate);
                if (entry == null)
                {
                    await AddAsync(entity);
                }
            }
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public T Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
