using Easy.NHibernate.UnitTests.DataSource.Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Easy.NHibernate.UnitTests.DataSource.Persistence
{
    public class SkillsEnhancementAllowanceMappings : JoinedSubclassMapping<SkillsEnhancementAllowance>
    {
        public SkillsEnhancementAllowanceMappings()
        {
            Key(k => k.Column("Id"));
            Property(s => s.Entitlement);
            Property(s => s.RemainingEntitlement);
        }
    }
}