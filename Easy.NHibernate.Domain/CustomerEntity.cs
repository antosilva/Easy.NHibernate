using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.NHibernate.Domain
{
    public class CustomerEntity : EntityBase<CustomerEntity>
    {
        public virtual string Name { get; set; }
    }
}
