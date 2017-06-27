using System;
using Easy.NHibernate.Database.Configurations;
using Easy.NHibernate.Database.Mappings.Interfaces;
using Easy.NHibernate.Database.Schema;
using Easy.NHibernate.Database.Session;
using Easy.NHibernate.Database.Session.Interfaces;
using Easy.NHibernate.Database.Store;
using Easy.NHibernate.Database.Store.Interfaces;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Mappings;
using NHibernate;
using NHibernate.Cfg;

namespace Easy.NHibernate.UnitTests.Populate
{
    internal class PopulateData
    {
        public IModelMappings ModelMappings { get; }
        public ISessionManager SessionManager { get; }

        public PopulateData()
        {
            Configuration configuration = new SqliteConfiguration("Data Source=UnitTest.db; Version=3;");

            ModelMappings = new Database.Mappings.ModelMappings(configuration);
            ModelMappings.AddMappings(typeof(AddressMappings).Namespace);
            ModelMappings.CompileMappings();

            SessionManager = new CurrentSessionContextManager(configuration);

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

            using (ISession session = SessionManager.CurrentSession())
            using (var trans = session.BeginTransaction())
            {
                session.SaveOrUpdate(johnSmith);
                trans.Commit();
            }
        }
    }
}
