using Easy.NHibernate.Domain;
using NHibernate;

namespace Easy.NHibernate.Query.Interfaces
{
    public interface IQuery<TEntity, out TResult> where TEntity : EntityBase<TEntity>
    {
        TResult Run(IQueryOver<TEntity, TEntity> queryover);
    }
}
