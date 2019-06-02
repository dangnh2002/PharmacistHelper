using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PharmacistHelper.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        T GetSingle(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T Insert(T entity);
        void InsertIfNotExist(Expression<Func<T, Guid>> ids, List<T> entities);
        T Update(T entity);
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isDisableTracking = true);
        Task<T> AddAsync(T entity);
        Task InsertIfNotExistAsync(Expression<Func<T, Guid>> ids, List<T> entities);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> UpdateAsync(T entity);
        Task CommitAsync();
        void Commit();
    }
}
