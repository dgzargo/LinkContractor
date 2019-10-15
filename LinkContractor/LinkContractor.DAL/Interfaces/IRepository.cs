using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinkContractor.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(object key);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        void Update(TEntity entity);
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        void Add(params TEntity[] entities);
        void Remove(object key);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        void Remove(params TEntity[] entities);
    }
}