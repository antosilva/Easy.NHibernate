using System.Collections.Generic;
using System.Linq;
using Easy.NHibernate.Domain;

namespace Easy.NHibernate.Query.Interfaces
{
    public interface IQueryData<out TResult>
    {
        IEnumerable<TResult> Execute<TEntity>(IQueryable<TEntity> query) where TEntity : EntityBase<TEntity>;
    }
}
