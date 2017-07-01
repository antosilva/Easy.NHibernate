using System.Collections.Generic;
using Easy.NHibernate.Domain;

namespace Easy.NHibernate.Query.Interfaces
{
    public interface IQueryRunner
    {
        IEnumerable<TResult> Run<TEntity, TResult>(IQueryData<TResult> queryData) where TEntity : EntityBase<TEntity>;
    }
}
