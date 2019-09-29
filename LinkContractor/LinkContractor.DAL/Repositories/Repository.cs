using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace LinkContractor.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected Repository(DbContext context)
        {
            Entities = context.Set<TEntity>();
        }

        private DbSet<TEntity> Entities { get; }

        public TEntity Find(object key)
        {
            return Entities.Find(key);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) query = orderBy(query);

            return query.ToList();
        }

        public void Update(TEntity entity)
        {
            Entities.Update(entity);
        }

        #region ADD methods

        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public void Add(params TEntity[] entities)
        {
            Entities.AddRange(entities);
        }

        #endregion

        #region REMOVE methods

        public void Remove(object key)
        {
            var entity = Find(key);
            Entities.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        public void Remove(params TEntity[] entities)
        {
            Entities.RemoveRange(entities);
        }

        #endregion
    }
}