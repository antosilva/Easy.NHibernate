using System.Collections.Generic;
using System.Linq;
using Easy.NHibernate.Repository;
using Easy.NHibernate.UnitTests.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Easy.NHibernate.UnitTests.Repositories
{
    internal class CustomersRepository : Repository<CustomerEntity>
    {
        public CustomersRepository(ISession session)
            : base(session)
        {
        }

        public CustomerEntity QueryCustomer(int id)
        {
            return GetAll(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<CustomerEntity> QueryAllCustomers()
        {
            return GetAll();
        }

        public IEnumerable<CustomerEntity> QueryCustomersWithNameLike(string nameLike)
        {
            return GetAll(x => x.Name.IsLike(nameLike));
        }
    }
}
