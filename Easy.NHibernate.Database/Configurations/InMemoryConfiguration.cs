namespace Easy.NHibernate.Database.Configurations
{
    public class InMemoryConfiguration : SqliteConfiguration
    {
        public InMemoryConfiguration(string connectionString = "data source=:memory:")
            : base(connectionString)
        {
        }
    }
}
