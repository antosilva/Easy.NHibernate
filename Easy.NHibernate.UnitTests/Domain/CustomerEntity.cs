using Easy.NHibernate.Database.Domain;

namespace Easy.NHibernate.UnitTests.Domain
{
    /// <summary>
    /// Sample entity.
    /// </summary>
    public class CustomerEntity : EntityBase<CustomerEntity>
    {
        public virtual string Name { get; set; }
    }
}
