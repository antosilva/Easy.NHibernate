using System;
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
            IQueryOver<TEntity, TEntity> queryOver = _session.QueryOver<TEntity>();
            return query.Run(queryOver);
        }

        public TResult Run<TEntity, TResult>(Func<IQueryOver<TEntity, TEntity>, TResult> query) where TEntity : EntityBase<TEntity>
        {
            IQueryOver<TEntity, TEntity> queryOver = _session.QueryOver<TEntity>();
            return query(queryOver);
        }

        public IQueryOver<TEntity, TEntity> QueryOver<TEntity>() where TEntity : EntityBase<TEntity>
        {
            IQueryOver<TEntity, TEntity> queryOver = _session.QueryOver<TEntity>();
            return queryOver;
        }
    }
}
