using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Easy.NHibernate.Database.Domain.Interfaces;

namespace Easy.NHibernate.Database.Repository.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        void Add(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        T Get(int id);
        IEnumerable<T> Get(IEnumerable<int> ids);

        int Count();
        int Count(Expression<Func<T, bool>> criteria);

        IEnumerable<T> Query(Expression<Func<T, bool>> criteria);
        IEnumerable<T> Query(Expression<Func<T, bool>> criteria, int pageNumber, int itemsPerPage);
    }
}
