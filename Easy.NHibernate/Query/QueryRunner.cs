using System.Collections.Generic;
using Easy.NHibernate.Domain;
using Easy.NHibernate.Query.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace Easy.NHibernate.Query
{
    public class QueryRunner : IQueryRunner
    {
        private readonly ISession _session;

        public QueryRunner(ISession session)
        {
            _session = session;
        }

        public IEnumerable<TResult> Run<TEntity, TResult>(IQueryData<TResult> queryData) where TEntity : EntityBase<TEntity>
        {
            return queryData.Execute(_session.Query<TEntity>());
        }
    }
}
