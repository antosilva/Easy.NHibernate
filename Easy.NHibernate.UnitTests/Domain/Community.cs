﻿using System.Collections.Generic;
using Easy.NHibernate.Database.Domain;

namespace Easy.NHibernate.UnitTests.Domain
{
    public class Community : EntityBase<Community>
    {
        public Community()
        {
            Members = new HashSet<Employee>();
        }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<Employee> Members { get; set; }

        public virtual void AddMember(Employee employee)
        {
            employee.AddCommunity(this);
        }
    }
}