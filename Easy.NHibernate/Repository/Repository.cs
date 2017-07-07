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
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public void SaveOrUpdate(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void SaveOrUpdate(IEnumerable<T> entities)
        {
            foreach (T entity in entities.ToArray())
            {
                SaveOrUpdate(entity);
            }
        }

        public void Update(T entity)
        {
            _session.Merge(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities.ToArray())
            {
                Update(entity);
            }
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities.ToArray())
            {
                Delete(entity);
            }
        }

        public T GetById(int id)
        {
            return _session.Get<T>(id);
        }

        public IEnumerable<T> GetByIdIn(IEnumerable<int> ids)
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

        public IEnumerable<T> GetAllBetween(int startId, int endId)
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

        public IEnumerable<int> GetAllIds()
        {
            return QueryOver().OrderBy(x => x.Id).Asc
                              .Select(x => x.Id)
                              .List<int>();
        }

        public IEnumerable<int> GetAllIds(Expression<Func<T, bool>> criteria)
        {
            return QueryOver().Where(criteria)
                              .OrderBy(x => x.Id).Asc
                              .Select(x => x.Id)
                              .List<int>();
        }

        public IQueryOver<T, T> QueryOver()
        {
            return _session.QueryOver<T>();
        }
    }
}
