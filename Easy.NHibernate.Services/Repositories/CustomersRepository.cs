using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.NHibernate.Domain;
using Easy.NHibernate.Repository;
using NHibernate;

namespace Easy.NHibernate.Services.Repositories
{
    public class CustomersRepository : Repository<CustomerEntity>
    {
        public CustomersRepository(ISession session)
            : base(session)
        {
        }

        public IEnumerable<CustomerEntity> CustomersNameStartingWith(string nameStartingWith)
        {
            return Query(x => x.Name.StartsWith(nameStartingWith));
        }
    }
}
