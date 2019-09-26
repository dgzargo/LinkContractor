using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinkContractor.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Find(object key);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}