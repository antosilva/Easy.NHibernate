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

        public void Add(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            foreach (T entity in entities.ToArray())
            {
                Add(entity);
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

        public T Get(int id)
        {
            return _session.Get<T>(id);
        }

        public IEnumerable<T> Get(IEnumerable<int> ids)
        {
            return _session.Query<T>().Where(x => ids.Contains(x.Id));
        }

        public int Count()
        {
            return _session.Query<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _session.Query<T>().Where(criteria).Count();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> criteria)
        {
            return _session.Query<T>().Where(criteria).ToList();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> criteria, int pageNumber, int itemsPerPage)
        {
            return _session.Query<T>()
                           .Where(criteria)
                           .Skip(pageNumber * itemsPerPage)
                           .Take(itemsPerPage)
                           .ToList();
        }
    }
}
