using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Easy.NHibernate.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Add(IEnumerable<T> entity);

        void Update(T entity);
        void Update(IEnumerable<T> entity);

        void Delete(T entity);
        void Delete(IEnumerable<T> entity);

        T Get(int id);

        IEnumerable<T> Query(Expression<Func<T, bool>> criteria);
        IEnumerable<T> Query(Expression<Func<T, bool>> criteria, int pageNumber, int itemsPerPage);
    }
}
