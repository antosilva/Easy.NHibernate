using System;
using Easy.NHibernate.Domain;

namespace Easy.NHibernate.UnitTests.Domain
{
    public class CustomerEntity : EntityBase<CustomerEntity>
    {
        public virtual string Name { get; set; }
        public virtual DateTime PaymentDate { get; set; }
    }
}
