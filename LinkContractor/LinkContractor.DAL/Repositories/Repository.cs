using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace LinkContractor.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DbContext context)
        {
            Entities = context.Set<T>();
        }

        private DbSet<T> Entities { get; }

        public T Find(object key)
        {
            return Entities.Find(key);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Entities.AddRange(entities);
        }

        public void Remove(T entity)
        {
            Entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Entities.RemoveRange(entities);
        }
    }
}