using Easy.NHibernate.Domain;

namespace Easy.NHibernate.Query.Interfaces
{
    public interface IQueryRunner
    {
        TResult Run<TEntity, TResult>(IQuery<TEntity, TResult> query) where TEntity : EntityBase<TEntity>;
    }
}
