using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Easy.NHibernate.Domain.Interfaces;
using Easy.NHibernate.Repository.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Easy.NHibernate.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public void Add(T entity)
        {
            _session.Save(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            foreach (T entity in entities.ToArray())
            {
                Add(entity);
            }
        }

        public void Remove(T entity)
        {
            _session.Delete(entity);
        }

        public void Remove(IEnumerable<T> entities)
        {
            foreach (T entity in entities.ToArray())
            {
                Remove(entity);
            }
        }

        public T GetById(long id)
        {
            return _session.Get<T>(id);
        }

        public IEnumerable<T> GetByIdIn(IEnumerable<long> ids)
        {
            return QueryOver().Where(x => x.Id.IsIn(ids.ToArray()))
                              .OrderBy(x => x.Id).Asc
                              .List();
        }

        public int Count()
        {
            return QueryOver().RowCount();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return QueryOver().Where(criteria)
                              .RowCount();
        }

        public IEnumerable<T> GetByIdBetween(long startId, long endId)
        {
            return QueryOver().Where(x => x.Id.IsBetween(startId).And(endId))
                              .OrderBy(x => x.Id).Asc
                              .List();
        }

        public IEnumerable<T> GetAll()
        {
            return QueryOver().OrderBy(x => x.Id).Asc
                              .List();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> criteria)
        {
            return QueryOver().Where(criteria)
                              .OrderBy(x => x.Id).Asc
                              .List();
        }

        public IEnumerable<long> GetAllIds()
        {
            return QueryOver().OrderBy(x => x.Id).Asc
                              .Select(x => x.Id)
                              .List<long>();
        }

        public IEnumerable<long> GetAllIds(Expression<Func<T, bool>> criteria)
        {
            return QueryOver().Where(criteria)
                              .OrderBy(x => x.Id).Asc
                              .Select(x => x.Id)
                              .List<long>();
        }

        public IQueryOver<T, T> QueryOver()
        {
            return _session.QueryOver<T>();
        }
    }
}
