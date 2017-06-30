using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Easy.NHibernate.Database.Domain.Interfaces;

namespace Easy.NHibernate.Database.Repository.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Save(T entity);
        void Save(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        T GetById(int id);
        IEnumerable<T> GetByIdIn(IEnumerable<int> ids);

        IEnumerable<int> GetAllIds();
        IEnumerable<int> GetAllIds(Expression<Func<T, bool>> criteria);

        int Count();
        int Count(Expression<Func<T, bool>> criteria);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> criteria);

        // For paging.
        IEnumerable<T> GetAllBetween(int startId, int endId);
    }
}
