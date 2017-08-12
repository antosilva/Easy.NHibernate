using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UserType;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace Easy.NHibernate.UnitTests.Mappings
{
    internal class CustomerMapping : ClassMapping<CustomerEntity>
    {
        public CustomerMapping()
        {
            Table("ta_customers");

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

            Property(x => x.PaymentDate,
                     m =>
                     {
                         m.Column("payment_date_time");
                         m.Type<DateTimeOffsetStringType>();
                     });
        }
    }
}
