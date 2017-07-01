using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Easy.NHibernate.Configurations;
using Easy.NHibernate.DataStore.Interfaces;
using Easy.NHibernate.Mappings;
using Easy.NHibernate.Mappings.Interfaces;
using Easy.NHibernate.Query;
using Easy.NHibernate.Query.Interfaces;
using Easy.NHibernate.Schema;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session;
using Easy.NHibernate.Session.Interfaces;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Mappings;
using Easy.NHibernate.UnitTests.Queries;
using Easy.NHibernate.UnitTests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Easy.NHibernate.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //#region Comments
            ////Configuration sqlite = new SqliteConfiguration("Data Source=mydb.db;Version=3;");
            ////IModelMappings databaseSession = new ModelMappings(sqlite);
            ////databaseSession.AddMappings(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            ////databaseSession.CompileMappings();
            ////SchemaExporter schlite = new SchemaExporter(sqlite);
            ////schlite.ExportToConsole();
            ////schlite.ExportToDatabase();
            ////using (ISession sqliteSession = databaseSession.OpenSession())
            ////{
            ////}
            ////return;

            ////InMemoryConfiguration sqlite = new InMemoryConfiguration();
            ////IModelMappings databaseSession = new ModelMappings(sqlite);
            ////databaseSession.AddMappings(new[] { Assembly.GetAssembly(typeof(CustomerMapping)) });
            ////databaseSession.CompileMappings();
            ////SchemaExporter schlite = new SchemaExporter(sqlite);
            ////schlite.ExportToConsole();
            ////schlite.ExportToDatabase();
            ////using (ISession sqlitesession = databaseSession.OpenSession())
            ////{
            ////}
            ////return;

            ////PopulateData td = new PopulateData();
            //#endregion

            ////Configuration cfg = new MsSqlConfiguration(@"Server=virgo\SQLEXPRESS;Database=testDB;Trusted_Connection=True;");
            //Configuration cfg = new InMemoryConfiguration();
            ////cfg.Cache(cache =>
            ////          {
            ////              cache.UseQueryCache = true;
            ////              cache.Provider<HashtableCacheProvider>();
            ////              //cache.QueryCache<StandardQueryCache>(); // Buggy, see hereafter for second level query cache.
            ////          });
            ////cfg.SetProperty(Environment.UseSecondLevelCache, "true");
            ////cfg.SetProperty(Environment.QueryCacheFactory, typeof(StandardQueryCacheFactory).FullName);

            //// Provides a CurrentSession in each thread using the ThreadStaticAttribute.
            //cfg.CurrentSessionContext<ThreadStaticSessionContext>();

            //// Provides a CurrentSession in each HttpContext, works only with web apps.
            ////cfg.CurrentSessionContext<WebSessionContext>();

            //// Provides a CurrentSession in each OperationContext in WCF, works only during the lifetime of a WCF operation.
            ////cfg.CurrentSessionContext<WcfOperationSessionContext>();

            //// Provides a CurrentSession in the CurrentDomain, works during the lifetime of the application.
            ////cfg.CurrentSessionContext<CurrentDomainSessionContext>();

            //IModelMappings modelMappings = new ModelMappings(cfg);
            //ISessionManager sessionManager = new SessionManager(cfg);
            //SchemaExport schemaExporter = new SchemaExport(cfg);

            //IDataStore dataStore = new DataStore.DataStore(modelMappings, sessionManager, schemaExporter);
            //dataStore.AddMappings(Assembly.GetAssembly(typeof(CustomerMapping)));
            //dataStore.CompileMappings();
            //dataStore.ExportToFile(@".\schema.sql");
            //dataStore.ExportToConsole();

            //ISession session = dataStore.CurrentSession();

            //IQueryData<int> q = new QueryIds();
            //IQueryRunner qr = new QueryRunner(session);
            //var v = qr.Run<CustomerEntity, int>(q);

            //CustomersRepository repo = new CustomersRepository(session);

            //int n = repo.Count();
            ////n = repo.Count(x => x.Name.StartsWith("D"));

            //var vv = repo.GetAllBetween(0, 2);

            //CustomerEntity customer = repo.QueryCustomer(30);
            //IEnumerable<CustomerEntity> all = repo.QueryAllCustomers();
            //IEnumerable<CustomerEntity> customers = repo.QueryCustomersWithNameLike("J%");

            //customer = repo.GetById(60);
            //var cs = repo.GetByIdIn(new List<int> {80, 81, 82, 3}).ToList();


            ////using (var uow = new UnitOfWork(session))
            ////{
            ////    CustomerEntity newCustomer = new CustomerEntity
            ////                                 {
            ////                                     Name = "TEST"
            ////                                 };
            ////    repo.Save(newCustomer);
            ////    uow.Commit();
            ////}
        }
    }
}
