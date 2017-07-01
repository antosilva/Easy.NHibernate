using System.Collections.Generic;
using System.Linq;
using Easy.NHibernate.Domain;
using Easy.NHibernate.Query.Interfaces;

namespace Easy.NHibernate.UnitTests.Queries
{
    public class QueryIds : IQueryData<int>
    {
        public IEnumerable<int> Execute<TEntity>(IQueryable<TEntity> query) where TEntity : EntityBase<TEntity>
        {
            return query.Select(x => x.Id).ToList();
        }
    }
}
