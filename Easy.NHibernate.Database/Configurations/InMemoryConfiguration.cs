using System;

namespace Easy.NHibernate.Database.Configurations
{
    public class InMemoryConfiguration : SqliteConfiguration
    {
        public InMemoryConfiguration(string connectionString = "Data Source=:memory:;Version=3;")
            : base(connectionString)
        {
            if (connectionString.ToLower().Contains("data source=:memory:") == false)
            {
                throw new ArgumentException("Invalid SQLite connection string, it must contain a memory data source declaration");
            }
        }
    }
}
