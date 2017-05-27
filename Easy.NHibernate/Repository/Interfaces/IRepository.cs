using System;
using System.Linq;
using System.Linq.Expressions;

namespace Easy.NHibernate.Repository.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Save(T entity);
        void Update(T entity);
        T GetById(long id);
        IQueryable<T> FindAll(Expression<Func<T, bool>> selector);
        IQueryable<T> FindAll(Expression<Func<T, bool>> selector, int recordsPerPage, int pageNumber);
    }
}
