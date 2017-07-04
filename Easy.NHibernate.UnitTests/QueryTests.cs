﻿using System;
using System.Linq;
using Easy.NHibernate.Configurations;
using Easy.NHibernate.DataStore.Interfaces;
using Easy.NHibernate.Mappings;
using Easy.NHibernate.Mappings.Interfaces;
using Easy.NHibernate.Query;
using Easy.NHibernate.Query.Interfaces;
using Easy.NHibernate.Session;
using Easy.NHibernate.Session.Interfaces;
using Easy.NHibernate.UnitTests.AAA;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Mappings;
using FluentAssertions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Easy.NHibernate.UnitTests
{
    internal class QueryTests : ArrangeActAssert
    {
        protected IDataStore DataStore;
        protected string Schema;
        protected CustomerEntity[] Customers;
        protected IQueryRunner QueryRunner;

        public override void Arrange()
        {
            Customers = new[]
                        {
                            new CustomerEntity {Name = "Joe", PaymentDate = DateTime.Today.AddDays(-5)},
                            new CustomerEntity {Name = "Dan", PaymentDate = DateTime.Today.AddDays(-2)},
                            new CustomerEntity {Name = "Jon", PaymentDate = DateTime.Today},
                            new CustomerEntity {Name = "Ric", PaymentDate = DateTime.Today.AddDays(+10)},
                        };

            Configuration configuration = new InMemoryConfiguration();
            configuration.DataBaseIntegration(di => { di.LogSqlInConsole = false; });

            IModelMappings mappings = new ModelMappings(configuration);
            ISessionManager sessionManager = new SessionManager(configuration, SessionContextAffinity.Local);
            SchemaExport schemaExporter = new SchemaExport(configuration);

            DataStore = new DataStore.DataStore(mappings, sessionManager, schemaExporter);
            DataStore.AddMappings(typeof(CustomerMapping));
            DataStore.CompileMappings();
            Schema = DataStore.ExportToDatabase();

            QueryRunner = new QueryRunner(DataStore.CurrentSession());

            Populate();
        }

        protected void Populate()
        {
            ISession session = DataStore.CurrentSession();
            foreach (CustomerEntity customer in Customers)
            {
                session.Save(customer);
            }
        }

        public override void OneTimeTearDown()
        {
            // Cleanup our "local" session.
            ISession session = DataStore.UnbindCurrentSession();
            session.Dispose();
        }

        [Test]
        public void Assert_all_instances_have_been_saved()
        {
            ISession session = DataStore.CurrentSession();
            int count = session.QueryOver<CustomerEntity>().List().Count;
            count.Should().Be(Customers.Length);
        }
    }

    internal class QueryTests_count_entities : QueryTests
    {
        protected int CountAll;
        protected int CountAllForNameLike;

        internal class CountAllIds : IQuery<CustomerEntity, int>
        {
            public int Run(IQueryOver<CustomerEntity, CustomerEntity> queryover)
            {
                return queryover.Select(x => x.Id).RowCount();
            }
        }

        internal class CountIdsForNameLike : IQuery<CustomerEntity, int>
        {
            private readonly string _likeName;

            public CountIdsForNameLike(string likeName)
            {
                _likeName = likeName;
            }

            public int Run(IQueryOver<CustomerEntity, CustomerEntity> queryover)
            {
                return queryover.Where(x => x.Name.IsLike(_likeName)).Select(x => x.Id).RowCount();
            }
        }

        public override void Act()
        {
            CountAll = QueryRunner.Run(new CountAllIds());
            CountAllForNameLike = QueryRunner.Run(new CountIdsForNameLike("J%"));
        }

        [Test]
        public void Assert_Count_all_has_same_length_like_input_customers()
        {
            CountAll.Should().Be(Customers.Length);
        }

        [Test]
        public void Assert_Count_with_criteria_matches_customers_filtered_with_same_criteria()
        {
            CountAllForNameLike.Should().Be(Customers.Count(x => x.Name.StartsWith("J")));
        }
    }
}