using Easy.NHibernate.UnitTests.DataSource.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Easy.NHibernate.UnitTests.DataSource.Persistence
{
    public class AddressMappings : ClassMapping<Address>
    {
        public AddressMappings()
        {
            Id(a => a.Id, mapper => mapper.Generator(Generators.HighLow));
            Property(a => a.AddressLine1);
            Property(a => a.AddressLine2);
            Property(a => a.Postcode);
            Property(a => a.City);
            Property(a => a.Country);
            ManyToOne(a => a.Employee, mapper =>
            {
                mapper.Class(typeof (Employee));
                mapper.Column("Employee_Id");
                mapper.Unique(true);
                mapper.Lazy(LazyRelation.NoProxy);
            });
            Cache(c =>
            {
                c.Usage(CacheUsage.ReadWrite);
                c.Region("AddressEntries");
            });
        }
    }
}
