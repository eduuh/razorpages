using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _dbcontext;
        internal DbSet<T> dbset;

        public Repository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            this.dbset = dbcontext.Set<T>();
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T Get(string id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includproperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // iclude properties will be comman separtated
            if (includproperties != null)
            {
                foreach (var includeproperty in includproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
            }

            if (orderby != null)
            {
                return orderby(query).ToList();
            }
            return query.ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeproperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // iclude properties will be comman separtated
            if (includeproperties != null)
            {
                foreach (var includeproperty in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(string id)
        {
            T entitytoRemove = dbset.Find(id);
            Remove(entitytoRemove);
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }
    }
}