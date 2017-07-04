using Easy.NHibernate.Domain;
using Easy.NHibernate.Query.Interfaces;
using NHibernate;

namespace Easy.NHibernate.Query
{
    public class QueryRunner : IQueryRunner
    {
        protected readonly ISession _session;

        public QueryRunner(ISession session)
        {
            _session = session;
        }

        public TResult Run<TEntity, TResult>(IQuery<TEntity, TResult> query) where TEntity : EntityBase<TEntity>
        {
            return query.Run(_session.QueryOver<TEntity>());
        }
    }
}
