using System;
using System.Collections.Generic;
using System.Reflection;
using Easy.NHibernate.Database.Configurations;
using Easy.NHibernate.Database.Schema;
using Easy.NHibernate.Database.Sessions;
using Easy.NHibernate.Database.Sessions.Interfaces;
using Easy.NHibernate.Domain;
using Easy.NHibernate.Persistence.GenericRepository;
using Easy.NHibernate.Persistence.Mappings;
using Easy.NHibernate.Persistence.Repositories;
using Easy.NHibernate.UnitTests.DataSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
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
            //IDatabaseSessionFactory sqliteFactory = new DatabaseSessionFactory(sqlite);
            //sqliteFactory.AddMappingTypes(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            //sqliteFactory.CompileMappings();
            //DatabaseSchema schlite = new DatabaseSchema(sqlite);
            //schlite.ExportToConsole();
            //schlite.ExportToDatabase();
            //using (ISession sqlitesession = sqliteFactory.OpenSession())
            //{
            //}
            //return;

            InMemoryConfiguration sqlite = new InMemoryConfiguration();
            IDatabaseSessionFactory sqliteFactory = new DatabaseSessionFactory(sqlite);
            //sqliteFactory.AddMappingTypes(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            //sqliteFactory.CompileMappings();
            //DatabaseSchema schlite = new DatabaseSchema(sqlite);
            //schlite.ExportToConsole();
            //schlite.ExportToDatabase();
            //using (ISession sqlitesession = sqliteFactory.OpenSession())
            //{
            //}
            //return;

            TestingData td = new TestingData();

            Configuration msSqlConfiguration = new MsSqlConfiguration(@"Server=virgo\SQLEXPRESS;Database=testDB;Trusted_Connection=True;");

            IDatabaseSessionFactory sessionFactory = new DatabaseSessionFactory(msSqlConfiguration);

            sessionFactory.AddMappingTypes(new[] {Assembly.GetAssembly(typeof(CustomerMapping))});
            sessionFactory.CompileMappings();

            DatabaseSchema sch = new DatabaseSchema(msSqlConfiguration);
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

                //using (var uow = new UnitOfWork(session))
                //{
                //    CustomerEntity newCustomer = new CustomerEntity
                //                                 {
                //                                     Name = "TEST"
                //                                 };
                //    repo.Add(newCustomer);
                //    uow.Commit();
                //}
            }
        }
    }
}
