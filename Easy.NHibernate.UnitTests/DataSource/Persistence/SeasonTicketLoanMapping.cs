using Easy.NHibernate.UnitTests.DataSource.Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Easy.NHibernate.UnitTests.DataSource.Persistence
{
    public class SeasonTicketLoanMappings : JoinedSubclassMapping<SeasonTicketLoan>
    {
        public SeasonTicketLoanMappings()
        {
            Key(k => k.Column("Id"));
            Property(s => s.Amount);
            Property(s => s.MonthlyInstalment);
            Property(s => s.StartDate);
            Property(s => s.EndDate);
        }
    }
}
