using System;
using System.Linq;
using System.Linq.Expressions;
using Easy.NHibernate.Repository.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace Easy.NHibernate.Repository
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

        public void Update(T entity)
        {
            _session.Merge(entity);
        }

        public T GetById(long id)
        {
            return _session.Load<T>(id);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> selector)
        {
            return _session.Query<T>()
                           .Where(selector);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> selector, int recordsPerPage, int pageNumber)
        {
            return _session.Query<T>()
                           .Where(selector)
                           .Skip(pageNumber * recordsPerPage)
                           .Take(recordsPerPage);
        }
    }
}
