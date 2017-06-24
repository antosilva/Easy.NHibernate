using System;
using Easy.NHibernate.Database.Configurations;
using Easy.NHibernate.Database.Schema;
using Easy.NHibernate.Database.Session;
using Easy.NHibernate.Database.Session.Interfaces;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Mappings;
using NHibernate;
using NHibernate.Cfg;

namespace Easy.NHibernate.UnitTests.Populate
{
    internal class PopulateData
    {
        public IDatabaseMappings DatabaseMappings { get; }
        public IDatabaseFacade DatabaseFacade { get; }

        public PopulateData()
        {
            Configuration configuration = new SqliteConfiguration("Data Source=UnitTest.db; Version=3;");

            DatabaseMappings = new DatabaseMappings(configuration);
            DatabaseMappings.AddMappings(typeof(AddressMappings).Namespace);
            DatabaseMappings.CompileMappings();

            DatabaseFacade = new Database.Session.DatabaseFacade(configuration);

            SchemaExporter dbSchemaExporter = new SchemaExporter(configuration);
            dbSchemaExporter.ExportToDatabase();

            Populate();
        }

        private void Populate()
        {
            var johnSmith = new Employee
                            {
                                Firstname = "John",
                                Lastname = "Smith",
                                DateOfJoining = new DateTime(2014, 5, 5),
                                EmployeeNumber = "empnum",
                                ResidentialAddress = new Address
                                                     {
                                                         AddressLine1 = "123 Planet place",
                                                         AddressLine2 = "12 Gomez street",
                                                         City = "London",
                                                         Postcode = "SW7 4FG",
                                                         Country = "United Kingdom"
                                                     }
                            };

            johnSmith.AddCommunity(new Community
                                   {
                                       Name = "NHibernate Beginners"
                                   });
            johnSmith.AddCommunity(new Community
                                   {
                                       Name = "London bikers"
                                   });
            johnSmith.AddBenefit(new SeasonTicketLoan
                                 {
                                     Amount = 1320,
                                     MonthlyInstalment = 110
                                 });
            johnSmith.AddBenefit(new Leave
                                 {
                                     AvailableEntitlement = 12,
                                     RemainingEntitlement = 2
                                 });

            using (ISession session = DatabaseFacade.CurrentSession())
            using (var trans = session.BeginTransaction())
            {
                session.SaveOrUpdate(johnSmith);
                trans.Commit();
            }
        }
    }
}
