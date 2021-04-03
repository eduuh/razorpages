using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(string Id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
         string includproperties = null
        );
        T GetFirstOrDefault(
        Expression<Func<T, bool>> filter = null, string includeproperties = null);
        void Add(T entity);
        void Remove(string id);
        void Remove(T entity);
    }
}