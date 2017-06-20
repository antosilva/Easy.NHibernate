using Easy.NHibernate.Database.Domain;

namespace Easy.NHibernate.UnitTests.DataSource.Domain
{
    public class Benefit : EntityBase<Benefit>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
