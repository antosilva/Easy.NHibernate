using Easy.NHibernate.UnitTests.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace Easy.NHibernate.UnitTests.Mappings
{
    public class LeaveMappings : JoinedSubclassMapping<Leave>
    {
        public LeaveMappings()
        {
            Key(k => k.Column("Id"));
            Property(l => l.Type, m => m.Type<EnumStringType<LeaveType>>());
            Property(l => l.AvailableEntitlement);
            Property(l => l.RemainingEntitlement);
        }
    }
}