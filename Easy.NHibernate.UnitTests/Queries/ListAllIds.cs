using System;
using System.Collections.Generic;
using System.Linq;
using Easy.NHibernate.Domain;
using Easy.NHibernate.Domain.Interfaces;
using Easy.NHibernate.Query;
using Easy.NHibernate.Query.Interfaces;
using Easy.NHibernate.UnitTests.Domain;
using NHibernate;

namespace Easy.NHibernate.UnitTests.Queries
{
    internal class ListAllIds : IQuery<CustomerEntity, IEnumerable<long>>
    {
        public IEnumerable<long> Run(IQueryOver<CustomerEntity, CustomerEntity> queryover)
        {
            return queryover.Select(x => x.Id).List<long>();
        }
    }
}
