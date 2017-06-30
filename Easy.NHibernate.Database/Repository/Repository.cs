using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Easy.NHibernate.Database.Domain.Interfaces;
using Easy.NHibernate.Database.Repository.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Easy.NHibernate.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public void Save(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Save(IEnumerable<T> entities)
        {
            foreach (T entity in entities.ToArray())
            {
                Save(entity);
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
            return _session.QueryOver<T>()
                           .Where(x => x.Id.IsIn(ids.ToArray()))
                           .OrderBy(x => x.Id)
                           .Asc
                           .List();
        }

        public int Count()
        {
            return _session.QueryOver<T>()
                           .RowCount();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _session.QueryOver<T>()
                           .Where(criteria)
                           .RowCount();
        }

        public IEnumerable<T> GetAllBetween(int startId, int endId)
        {
            return _session.QueryOver<T>()
                           .Where(x => x.Id.IsBetween(startId).And(endId))
                           .OrderBy(x => x.Id)
                           .Asc
                           .List();
        }

        //public IEnumerable<T> GetAllBetween(Expression<Func<T, bool>> criteria, int startId, int endId)
        //{
        //    return _session.QueryOver<T>()
        //                   .Where(criteria)
        //                   .And(x => x.Id.IsBetween(startId).And(endId))
        //                   .OrderBy(x => x.Id)
        //                   .Asc
        //                   .List();
        //}

        public IEnumerable<T> GetAll()
        {
            return _session.QueryOver<T>()
                           .OrderBy(x => x.Id)
                           .Asc
                           .List();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> criteria)
        {
            return _session.QueryOver<T>()
                           .Where(criteria)
                           .OrderBy(x => x.Id)
                           .Asc
                           .List();
        }

        public IEnumerable<int> GetAllIds()
        {
            return _session.QueryOver<T>()
                           .OrderBy(x => x.Id)
                           .Asc
                           .Select(x => x.Id)
                           .List<int>();
        }

        public IEnumerable<int> GetAllIds(Expression<Func<T, bool>> criteria)
        {
            return _session.QueryOver<T>()
                           .Where(criteria)
                           .OrderBy(x => x.Id)
                           .Asc
                           .Select(x => x.Id)
                           .List<int>();
        }
    }
}
