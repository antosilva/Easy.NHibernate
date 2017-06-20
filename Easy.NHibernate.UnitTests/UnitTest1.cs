using System.Collections.Generic;
using System.Reflection;
using Easy.NHibernate.Database.Configurations;
using Easy.NHibernate.Database.Repository;
using Easy.NHibernate.Database.Schema;
using Easy.NHibernate.Database.Session;
using Easy.NHibernate.Database.Session.Interfaces;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Mappings;
using Easy.NHibernate.UnitTests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;

namespace Easy.NHibernate.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Configuration sqlite = new SqliteConfiguration("Data Source=mydb.db;Version=3;");
            //IDatabaseSession databaseSession = new DatabaseSession(sqlite);
            //databaseSession.AddMappingTypes(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            //databaseSession.CompileMappings();
            //SchemaExporter schlite = new SchemaExporter(sqlite);
            //schlite.ExportToConsole();
            //schlite.ExportToDatabase();
            //using (ISession sqliteSession = databaseSession.OpenSession())
            //{
            //}
            //return;

            //InMemoryConfiguration sqlite = new InMemoryConfiguration();
            //IDatabaseSession databaseSession = new DatabaseSession(sqlite);
            //databaseSession.AddMappingTypes(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            //databaseSession.CompileMappings();
            //SchemaExporter schlite = new SchemaExporter(sqlite);
            //schlite.ExportToConsole();
            //schlite.ExportToDatabase();
            //using (ISession sqlitesession = databaseSession.OpenSession())
            //{
            //}
            //return;

            //PopulateData td = new PopulateData();

            Configuration msSqlConfiguration = new MsSqlConfiguration(@"Server=virgo\SQLEXPRESS;Database=testDB;Trusted_Connection=True;");

            IDatabaseSession sessionFactory = new DatabaseSession(msSqlConfiguration);

            sessionFactory.AddMappingTypes(new[] {Assembly.GetAssembly(typeof(CustomerMapping))});
            sessionFactory.CompileMappings();

            SchemaExporter sch = new SchemaExporter(msSqlConfiguration);
            sch.ExportToFile(@".\schema.sql");
            sch.ExportToConsole();

            using (var session = sessionFactory.OpenSession())
            {
                CustomersRepository repo = new CustomersRepository(session);

                CustomerEntity customer = repo.QueryCustomer(30);
                IEnumerable<CustomerEntity> all = repo.QueryAllCustomers();
                IEnumerable<CustomerEntity> customers = repo.QueryCustomersNameStartingWith("R");

                customer = repo.Get(60);
                customers = repo.Get(new List<int> {80, 81, 82});

                using (var uow = new UnitOfWork(session))
                {
                    CustomerEntity newCustomer = new CustomerEntity
                    {
                        Name = "TEST"
                    };
                    repo.Add(newCustomer);
                    uow.Commit();
                }
            }
        }
    }
}
