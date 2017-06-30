using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Easy.NHibernate.Database.Configurations;
using Easy.NHibernate.Database.Mappings;
using Easy.NHibernate.Database.Mappings.Interfaces;
using Easy.NHibernate.Database.Repository;
using Easy.NHibernate.Database.Schema;
using Easy.NHibernate.Database.Schema.Interfaces;
using Easy.NHibernate.Database.Session;
using Easy.NHibernate.Database.Session.Interfaces;
using Easy.NHibernate.Database.Store;
using Easy.NHibernate.Database.Store.Interfaces;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Mappings;
using Easy.NHibernate.UnitTests.Populate;
using Easy.NHibernate.UnitTests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Criterion;

namespace Easy.NHibernate.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            #region Comments
            //Configuration sqlite = new SqliteConfiguration("Data Source=mydb.db;Version=3;");
            //IModelMappings databaseSession = new ModelMappings(sqlite);
            //databaseSession.AddMappings(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            //databaseSession.CompileMappings();
            //SchemaExporter schlite = new SchemaExporter(sqlite);
            //schlite.ExportToConsole();
            //schlite.ExportToDatabase();
            //using (ISession sqliteSession = databaseSession.OpenSession())
            //{
            //}
            //return;

            //InMemoryConfiguration sqlite = new InMemoryConfiguration();
            //IModelMappings databaseSession = new ModelMappings(sqlite);
            //databaseSession.AddMappings(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            //databaseSession.CompileMappings();
            //SchemaExporter schlite = new SchemaExporter(sqlite);
            //schlite.ExportToConsole();
            //schlite.ExportToDatabase();
            //using (ISession sqlitesession = databaseSession.OpenSession())
            //{
            //}
            //return;

            //PopulateData td = new PopulateData();
            #endregion

            //Configuration cfg = new MsSqlConfiguration(@"Server=virgo\SQLEXPRESS;Database=testDB;Trusted_Connection=True;");
            Configuration cfg = new InMemoryConfiguration();
            //cfg.Cache(cache =>
            //          {
            //              cache.UseQueryCache = true;
            //              cache.Provider<HashtableCacheProvider>();
            //              //cache.QueryCache<StandardQueryCache>(); // Buggy, see hereafter for second level query cache.
            //          });
            //cfg.SetProperty(Environment.UseSecondLevelCache, "true");
            //cfg.SetProperty(Environment.QueryCacheFactory, typeof(StandardQueryCacheFactory).FullName);

            IModelMappings modelMappings = new ModelMappings(cfg);
            ISessionManager sessionManager = new CurrentSessionContextManager(cfg);
            ISchemaExporter schemaExporter = new SchemaExporter(cfg);

            IDataStore dataStore = new DataStore(modelMappings, sessionManager, schemaExporter);
            dataStore.AddMappings(Assembly.GetAssembly(typeof(CustomerMapping)));
            dataStore.CompileMappings();
            dataStore.ExportToFile(@".\schema.sql");
            dataStore.ExportToConsole();

            ISession session = dataStore.CurrentSession();

            string schema = dataStore.ExportToDatabase(session);

            CustomersRepository repo = new CustomersRepository(session);

            int n = repo.Count();
            //n = repo.Count(x => x.Name.StartsWith("D"));

            var vv = repo.GetAllBetween(0, 2);

            CustomerEntity customer = repo.QueryCustomer(30);
            IEnumerable<CustomerEntity> all = repo.QueryAllCustomers();
            IEnumerable<CustomerEntity> customers = repo.QueryCustomersWithNameLike("J%");

            customer = repo.GetById(60);
            var cs = repo.GetByIdIn(new List<int> {80, 81, 82, 3}).ToList();

            //using (var uow = new UnitOfWork(session))
            //{
            //    CustomerEntity newCustomer = new CustomerEntity
            //                                 {
            //                                     Name = "TEST"
            //                                 };
            //    repo.Save(newCustomer);
            //    uow.Commit();
            //}
        }
    }
}
