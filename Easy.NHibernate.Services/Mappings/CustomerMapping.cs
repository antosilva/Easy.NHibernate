using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.NHibernate.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Easy.NHibernate.Services.Mappings
{
    public class CustomerMapping : ClassMapping<CustomerEntity>
    {
        public CustomerMapping()
        {
            Table("tbl_customers");

            Id(x => x.Id,
               m =>
               {
                   m.Column("id");
                   m.Generator(Generators.Identity); // Id is created at database level
               });

            Property(x => x.Name,
                     m =>
                     {
                         m.Column("name");
                     });
        }
    }
}
