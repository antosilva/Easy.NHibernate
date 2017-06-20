﻿using System.Collections.Generic;
using System.Linq;
using Easy.NHibernate.Database.Repository;
using Easy.NHibernate.UnitTests.Domain;
using NHibernate;

namespace Easy.NHibernate.UnitTests.Repositories
{
    public class CustomersRepository : Repository<CustomerEntity>
    {
        public CustomersRepository(ISession session)
            : base(session)
        {
        }

        public CustomerEntity QueryCustomer(int id)
        {
            return Query(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<CustomerEntity> QueryAllCustomers()
        {
            return Query(x => true);
        }

        public IEnumerable<CustomerEntity> QueryCustomersNameStartingWith(string nameStartingWith)
        {
            return Query(x => x.Name.StartsWith(nameStartingWith));
        }
    }
}