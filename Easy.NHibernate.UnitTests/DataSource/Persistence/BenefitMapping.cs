using Easy.NHibernate.UnitTests.DataSource.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Easy.NHibernate.UnitTests.DataSource.Persistence
{
    public class BenefitMappings : ClassMapping<Benefit>
    {
        public BenefitMappings()
        {
            Id(b => b.Id, idmapper => idmapper.Generator(Generators.HighLow));
            Property(b => b.Name);
            Property(b => b.Description);
            ManyToOne(b => b.Employee,
                mapper =>
                {
                    mapper.Column("Employee_Id");
                    mapper.Class(typeof(Employee));
                    //mapper.Cascade(Cascade.All);
                    mapper.Lazy(LazyRelation.NoProxy);
                });
        }
    }
}