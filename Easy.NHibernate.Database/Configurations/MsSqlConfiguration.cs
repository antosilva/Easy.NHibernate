using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Easy.NHibernate.Database.Configurations
{
    public class MsSqlConfiguration : Configuration
    {
        public MsSqlConfiguration(string connectionString)
        {
            this.DataBaseIntegration(di =>
                                     {
                                         di.ConnectionString = connectionString;
                                         di.Driver<Sql2008ClientDriver>();
                                         di.Dialect<MsSql2012Dialect>();
                                         di.LogFormattedSql = true;
                                         di.LogSqlInConsole = true;
                                     });
        }
    }
}
