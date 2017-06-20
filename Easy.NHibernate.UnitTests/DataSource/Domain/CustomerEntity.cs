using Easy.NHibernate.Domain;

namespace Easy.NHibernate.UnitTests.DataSource.Domain
{
    /// <summary>
    /// Sample entity.
    /// </summary>
    public class CustomerEntity : EntityBase<CustomerEntity>
    {
        public virtual string Name { get; set; }
    }
}
