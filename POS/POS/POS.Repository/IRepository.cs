using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace POS.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        T Get(string id);
        IQueryable<T> Get(int page, int size);
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, int page, int size);

        void Add(T item);
        void Add(IEnumerable<T> items);

        void Remove(T item);
        void Remove(IEnumerable<T> items);

        int Save();
    }
}
