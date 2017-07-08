using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Easy.NHibernate.Config
{
    public class SqliteConfiguration : Configuration
    {
        public SqliteConfiguration(string connectionString)
        {
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
