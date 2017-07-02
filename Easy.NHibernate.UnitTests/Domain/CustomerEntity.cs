﻿using System;
using Easy.NHibernate.Domain;

namespace Easy.NHibernate.UnitTests.Domain
{
    internal class CustomerEntity : EntityBase<CustomerEntity>
    {
        public virtual string Name { get; set; }
        public virtual DateTime PaymentDate { get; set; }

        /// <summary>
        /// Only for unit tests.
        /// </summary>
        public virtual void ChangeId(int id)
        {
            Id = id;
        }
    }
}
