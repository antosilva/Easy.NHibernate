using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Easy.NHibernate.Configurations;
using Easy.NHibernate.DataStore.Interfaces;
using Easy.NHibernate.Mappings;
using Easy.NHibernate.Mappings.Interfaces;
using Easy.NHibernate.Query;
using Easy.NHibernate.Query.Interfaces;
using Easy.NHibernate.Repository;
using Easy.NHibernate.Schema;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session;
using Easy.NHibernate.Session.Interfaces;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Mappings;
using Easy.NHibernate.UnitTests.Queries;
using Easy.NHibernate.UnitTests.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Easy.NHibernate.UnitTests
{
    [TestFixture]
    internal class Sandbox
    {
        [Test]
        public void TestMethod1()
        {
            #region Comments
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
            ISessionManager sessionManager = new SessionManager(cfg, SessionContextAffinity.CurrentThread);
            SchemaExport schemaExporter = new SchemaExport(cfg);

            IDataStore dataStore = new DataStore.DataStore(modelMappings, sessionManager, schemaExporter);
            dataStore.AddMappings(Assembly.GetAssembly(typeof(CustomerMapping)));
            dataStore.CompileMappings();
            dataStore.ExportToDatabase();

            ISession session = dataStore.CurrentSession();

            using (var uow = new UnitOfWork(session))
            {
                CustomersRepository repo = new CustomersRepository(session);
                CustomerEntity newCustomer = new CustomerEntity
                {
                    Name = "TEST"
                };
                repo.Save(newCustomer);
                uow.Commit();
            }

            // Query API instead of Repository pattern.
            IQueryRunner runner = new QueryRunner(session);
            ListAllIds query = new ListAllIds();
            IEnumerable<int> result = runner.Run(query);

            result.Count().Should().Be(1);
        }
    }
}
