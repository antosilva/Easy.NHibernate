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
using NHibernate.Context;

namespace Easy.NHibernate.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Configuration sqlite = new SqliteConfiguration("Data Source=mydb.db;Version=3;");
            //IDatabaseMappings databaseFacade = new DatabaseMappings(sqlite);
            //databaseFacade.AddMappings(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            //databaseFacade.CompileMappings();
            //SchemaExporter schlite = new SchemaExporter(sqlite);
            //schlite.ExportToConsole();
            //schlite.ExportToDatabase();
            //using (ISession sqliteSession = databaseFacade.OpenSession())
            //{
            //}
            //return;

            //InMemoryConfiguration sqlite = new InMemoryConfiguration();
            //IDatabaseMappings databaseFacade = new DatabaseMappings(sqlite);
            //databaseFacade.AddMappings(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            //databaseFacade.CompileMappings();
            //SchemaExporter schlite = new SchemaExporter(sqlite);
            //schlite.ExportToConsole();
            //schlite.ExportToDatabase();
            //using (ISession sqlitesession = databaseFacade.OpenSession())
            //{
            //}
            //return;

            //PopulateData td = new PopulateData();

            Configuration cfg = new MsSqlConfiguration(@"Server=virgo\SQLEXPRESS;Database=testDB;Trusted_Connection=True;");
            cfg.CurrentSessionContext<ThreadStaticSessionContext>();

            IDatabaseMappings mappings = new DatabaseMappings(cfg);
            mappings.AddMappings(new[] {Assembly.GetAssembly(typeof(CustomerMapping))});
            mappings.CompileMappings();

            SchemaExporter sch = new SchemaExporter(cfg);
            sch.ExportToFile(@".\schema.sql");
            sch.ExportToConsole();

            IDatabaseFacade databaseFacade = new DatabaseFacade(cfg);

            using (var session = databaseFacade.CurrentSession())
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
