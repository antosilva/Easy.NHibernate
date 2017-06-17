﻿using System.Collections.Generic;
using System.Reflection;
using Easy.NHibernate.Database;
using Easy.NHibernate.Database.Interfaces;
using Easy.NHibernate.Domain;
using Easy.NHibernate.Persistence.Mappings;
using Easy.NHibernate.Persistence.Repositories;
using Easy.NHibernate.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Easy.NHibernate.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IDatabaseSession db = new MsSqlSession(@"Server=virgo\SQLEXPRESS;Database=testDB;Trusted_Connection=True;");

            db.AddExportedMappingTypes(new[] {Assembly.GetAssembly(typeof(CustomerMapping))});

            using (var session = db.OpenSession())
            {
                CustomersRepository repo = new CustomersRepository(session);

                CustomerEntity curtomer = repo.QueryCustomer(30);
                IEnumerable<CustomerEntity> all = repo.QueryAllCustomers();
                IEnumerable<CustomerEntity> customers = repo.QueryCustomersNameStartingWith("R");

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
