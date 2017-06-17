namespace Easy.NHibernate.UnitTests.DataSource.Domain
{
    public class SkillsEnhancementAllowance : Benefit
    {
        public virtual int RemainingEntitlement { get; set; }
        public virtual int Entitlement { get; set; }
    }
}
