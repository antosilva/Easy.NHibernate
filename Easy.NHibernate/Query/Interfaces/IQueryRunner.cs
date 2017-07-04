using System;
using Easy.NHibernate.Domain;
using NHibernate;

namespace Easy.NHibernate.Query.Interfaces
{
    public interface IQueryRunner
    {
        TResult Run<TEntity, TResult>(IQuery<TEntity, TResult> query) where TEntity : EntityBase<TEntity>;
    }
}
