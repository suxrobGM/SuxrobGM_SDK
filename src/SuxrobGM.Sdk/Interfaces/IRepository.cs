using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuxrobGM.Sdk.Interfaces
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    public interface IRepository
    {
        Task<TEntity> GetByIdAsync<TEntity>(string id) where TEntity: class, IEntity<string>;
        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: class, IEntity<string>;
        Task<List<TEntity>> GetListAsync<TEntity>() where TEntity: class, IEntity<string>;
        Task<List<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: class, IEntity<string>;
        IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> predicate = null, 
            string includeString = null, bool disableTracking = true) where TEntity: class, IEntity<string>;
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity: class, IEntity<string>;
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity: class, IEntity<string>;
        Task DeleteAsync<TEntity>(TEntity entity) where TEntity: class, IEntity<string>;
    }
}
