using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Easy.NHibernate.Database.Configurations
{
    public class InMemoryConfiguration : Configuration
    {
        public InMemoryConfiguration(string connectionString = "Data Source=:memory:;Version=3;")
        {
            if (connectionString.ToLower().Contains("data source=:memory:") == false)
            {
                throw new ArgumentException("Invalid SQLite connection string, it must contain a memory data source declaration");
            }

            this.DataBaseIntegration(di =>
                                     {
                                         di.ConnectionString = connectionString;
                                         di.Dialect<SQLiteDialect>();
                                         di.Driver<SQLite20Driver>();
                                         di.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                                         di.LogFormattedSql = true;
                                         di.LogSqlInConsole = true;
                                     });
        }
    }
}
