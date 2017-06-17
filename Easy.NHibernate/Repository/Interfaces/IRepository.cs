using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Easy.NHibernate.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Add(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        IEnumerable<T> Query(Expression<Func<T, bool>> criteria);
        IEnumerable<T> Query(Expression<Func<T, bool>> criteria, int pageNumber, int itemsPerPage);
    }
}
