using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Easy.NHibernate.Domain.Interfaces;

namespace Easy.NHibernate.Repository.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        void Add(IEnumerable<T> entities);

        void Remove(T entity);
        void Remove(IEnumerable<T> entities);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> criteria);

        T GetById(long id);
        IEnumerable<T> GetByIdIn(IEnumerable<long> ids);
        IEnumerable<T> GetByIdBetween(long startId, long endId);

        IEnumerable<long> GetAllIds();
        IEnumerable<long> GetAllIds(Expression<Func<T, bool>> criteria);

        int Count();
        int Count(Expression<Func<T, bool>> criteria);
    }
}
