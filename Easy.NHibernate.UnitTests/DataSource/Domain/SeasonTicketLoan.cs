using System;

namespace Easy.NHibernate.UnitTests.DataSource.Domain
{
    public class SeasonTicketLoan : Benefit
    {
        public virtual int Amount { get; set; }
        public virtual double MonthlyInstalment { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
    }
}
