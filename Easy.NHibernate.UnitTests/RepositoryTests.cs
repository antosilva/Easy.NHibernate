using System;
using System.Collections.Generic;
using System.Linq;
using Easy.NHibernate.Config;
using Easy.NHibernate.DataStore.Interfaces;
using Easy.NHibernate.Mapping;
using Easy.NHibernate.Mapping.Interfaces;
using Easy.NHibernate.Repository;
using Easy.NHibernate.Repository.Interfaces;
using Easy.NHibernate.Schema;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session;
using Easy.NHibernate.Session.Interfaces;
using Easy.NHibernate.UnitTests.AAA;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Logger;
using Easy.NHibernate.UnitTests.Mappings;
using FluentAssertions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Easy.NHibernate.UnitTests
{
    internal class RepositoryTests : ArrangeActAssert
    {
        protected TestLogger Logger;
        protected IRepository<CustomerEntity> ObjectUnderTest;
        protected IDataStore DataStore;
        protected string Schema;
        protected CustomerEntity[] Customers;

        public override void Arrange()
        {
            Logger = new TestLogger(@"logs\" + GetType() + ".log");
            Logger.Log.InfoFormat("Stating Unit tests");

            Customers = new[]
                        {
                            new CustomerEntity {Name = "Joe", PaymentDate = DateTime.Today.AddDays(-5)},
                            new CustomerEntity {Name = "Dan", PaymentDate = DateTime.Today.AddDays(-2)},
                            new CustomerEntity {Name = "Jon", PaymentDate = DateTime.Today},
                            new CustomerEntity {Name = "Ric", PaymentDate = DateTime.Today.AddDays(+10)},
                        };

            //Configuration configuration = new MsSqlConfiguration(@"Server=virgo\SQLEXPRESS;Database=testDB;Trusted_Connection=True;");
            //Configuration configuration = new SqliteConfiguration("Data Source=testDB.db;Version=3;");

            Configuration configuration = new InMemoryConfiguration();
            configuration.DataBaseIntegration(di => { di.LogSqlInConsole = false; });

            IModelMappings mappings = new ModelMappings(configuration);
            ISessionManager sessionManager = new SessionManager(configuration, SessionContextAffinity.ThreadLocal); // ThreadLocal: works like a local variable.
            ISchemaExporter schemaExporter = new SchemaExporter(configuration, sessionManager);

            DataStore = new DataStore.DataStore(mappings, sessionManager, schemaExporter);
            DataStore.AddMappings(typeof(CustomerMapping));
            DataStore.CompileMappings();
            Schema = DataStore.ExportToDatabase();

            ObjectUnderTest = new Repository<CustomerEntity>(DataStore.CurrentSession);

            Populate();
        }

        protected void Populate()
        {
            foreach (CustomerEntity customer in Customers)
            {
                ObjectUnderTest.Add(customer);
            }
        }

        public override void OneTimeTearDown()
        {
            // Cleanup our "local" session.
            ISession session = DataStore.UnbindCurrentSession();
            session.Dispose();
        }
    }

    internal class RepositoryTests_count_entities : RepositoryTests
    {
        protected int CountAll;
        protected int CountAllWithCriteria;

        public override void Act()
        {
            CountAll = ObjectUnderTest.Count();
            CountAllWithCriteria = ObjectUnderTest.Count(x => x.Name.IsLike("J%"));
        }

        [Test]
        public void Assert_Count_all_has_same_length_like_input_customers()
        {
            CountAll.Should().Be(Customers.Length);
        }

        [Test]
        public void Assert_Count_with_criteria_matches_customers_filtered_with_same_criteria()
        {
            CountAllWithCriteria.Should().Be(Customers.Count(x => x.Name.StartsWith("J")));
        }
    }

    internal class RepositoryTests_get_by_id : RepositoryTests
    {
        protected CustomerEntity CustomerEntityById;
        protected IEnumerable<CustomerEntity> CustomerEntityByIdList;
        protected long[] Ids;

        public override void Arrange()
        {
            base.Arrange();
            Ids = new long[] {2, 3};
        }

        public override void Act()
        {
            CustomerEntityById = ObjectUnderTest.GetById(Ids.First());
            CustomerEntityByIdList = ObjectUnderTest.GetByIdIn(Ids);
        }

        [Test]
        public void Assert_GetById_should_return_known_customer()
        {
            CustomerEntityById.Should().Be(Customers.First(x => x.Id == Ids.First()));
        }

        [Test]
        public void Assert_GetByIdIn_should_return_known_customers()
        {
            CustomerEntityByIdList.Should().BeEquivalentTo(Customers.Where(x => Ids.Contains(x.Id)).ToList());
        }
    }

    internal class RepositoryTests_get_all_ids : RepositoryTests
    {
        protected IEnumerable<long> GetAllIds;
        protected IEnumerable<long> GetAllIdsByCriteria;

        public override void Act()
        {
            GetAllIds = ObjectUnderTest.GetAllIds();
            GetAllIdsByCriteria = ObjectUnderTest.GetAllIds(x => x.Name.IsLike("J%"));
        }

        [Test]
        public void Assert_GetAllIds_should_return_all_customer_ids()
        {
            GetAllIds.Should().BeEquivalentTo(Customers.Select(x => x.Id));
        }

        [Test]
        public void Assert_GetAllIds_with_criteria_should_return_filtered_customers_ids_with_same_criteria()
        {
            GetAllIdsByCriteria.Should().BeEquivalentTo(Customers.Where(x => x.Name.StartsWith("J")).Select(x => x.Id).ToList());
        }
    }

    internal class RepositoryTests_get_all_customers : RepositoryTests
    {
        protected IEnumerable<CustomerEntity> GetAllCustomers;
        protected IEnumerable<CustomerEntity> GetAllCustomersByCriteria;

        public override void Act()
        {
            GetAllCustomers = ObjectUnderTest.GetAll();
            GetAllCustomersByCriteria = ObjectUnderTest.GetAll(x => x.Name.IsLike("J%"));
        }

        [Test]
        public void Assert_GetAll_should_return_all_customers()
        {
            GetAllCustomers.Should().BeEquivalentTo(Customers.ToList());
        }

        [Test]
        public void Assert_GetAll_with_criteria_should_return_filtered_customers_with_same_criteria()
        {
            GetAllCustomersByCriteria.Should().BeEquivalentTo(Customers.Where(x => x.Name.StartsWith("J")).ToList());
        }
    }

    internal class RepositoryTests_get_all_between : RepositoryTests
    {
        protected IEnumerable<CustomerEntity> GetCustomersBetween;

        public override void Act()
        {
            GetCustomersBetween = ObjectUnderTest.GetByIdBetween(2, 10);
        }

        [Test]
        public void Assert_GetAllBetween_should_return_all_customers_between_limits()
        {
            GetCustomersBetween.Should().BeEquivalentTo(Customers.Where(x => x.Id >= 2 && x.Id <= 10).ToList());
        }
    }

    internal class RepositoryTests_updating_customer : RepositoryTests
    {
        protected long ModifiedCustomerId;
        protected CustomerEntity ModifiedCustomer;
        protected CustomerEntity CustomerRetrieved;

        public override void Act()
        {
            ModifiedCustomer = Customers.First();
            ModifiedCustomerId = ModifiedCustomer.Id;

            ISession session = DataStore.CurrentSession;
            using (IUnitOfWork uow = new UnitOfWork(session))
            {
                ModifiedCustomer = Customers.First();
                ModifiedCustomer.PaymentDate = DateTime.Today.AddDays(-2);
                uow.Complete();
            }

            ModifiedCustomer.PaymentDate = DateTime.Today;

            session = DataStore.CurrentSession;
            using (IUnitOfWork uow = new UnitOfWork(session))
            {
                CustomerRetrieved = ObjectUnderTest.GetById(ModifiedCustomerId);
                uow.Complete();
            }
        }

        [Test]
        public void Assert_the_update_has_been_saved_and_retrieved()
        {
            var e = new CustomerEntity { Name = ModifiedCustomer.Name, PaymentDate = DateTime.Now.Date.AddDays(-100) };
            e.ChangeId(ModifiedCustomerId);
            CustomerRetrieved.Should().Be(e);
        }
    }
}
