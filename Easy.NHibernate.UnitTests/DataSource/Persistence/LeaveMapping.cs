using Easy.NHibernate.UnitTests.DataSource.Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Easy.NHibernate.UnitTests.DataSource.Persistence
{
    public class LeaveMappings : JoinedSubclassMapping<Leave>
    {
        public LeaveMappings()
        {
            Key(k => k.Column("Id"));
            Property(l => l.Type);
            Property(l => l.AvailableEntitlement);
            Property(l => l.RemainingEntitlement);
        }
    }
}