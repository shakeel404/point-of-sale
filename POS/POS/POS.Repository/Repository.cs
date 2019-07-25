using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace POS.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity item)
        {
            Context.Set<TEntity>().Add(item);
        }

        public void Add(IEnumerable<TEntity> items)
        {
            Context.Set<TEntity>().AddRange(items);
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Get(int page, int size)
        {
            return Context.Set<TEntity>().Skip(size * (page - 1)).Take(size);
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>();
        }

        public TEntity Get(string id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, int page, int size)
        {
            return Context.Set<TEntity>().Where(predicate).Skip(size * (page - 1)).Take(size);
        }

        public void Remove(TEntity item)
        {
            Context.Set<TEntity>().Remove(item);
        }

        public void Remove(IEnumerable<TEntity> items)
        {
            Context.Set<TEntity>().RemoveRange(items);
        }

        public int Save()
        {
            return Context.SaveChanges();
        }
    }
}
