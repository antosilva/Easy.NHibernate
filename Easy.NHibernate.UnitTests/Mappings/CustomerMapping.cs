using Easy.NHibernate.UnitTests.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Easy.NHibernate.UnitTests.Mappings
{
    public class CustomerMapping : ClassMapping<CustomerEntity>
    {
        public CustomerMapping()
        {
            Table("ta_first");

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
