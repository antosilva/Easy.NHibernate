using Easy.NHibernate.UnitTests.Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Easy.NHibernate.UnitTests.Mappings
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