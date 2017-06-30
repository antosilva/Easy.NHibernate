using Easy.NHibernate.Database.Drivers.Sybase;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace Easy.NHibernate.Database.Configurations
{
    public class SybaseAseConfiguration : Configuration
    {
        public SybaseAseConfiguration(string connectionString)
        {
            this.DataBaseIntegration(di =>
                                     {
                                         di.ConnectionString = connectionString;
                                         di.Dialect<SybaseASE15Dialect>();
                                         di.Driver<SybaseAdoNet4AseDriver>();
                                         di.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                                         di.LogFormattedSql = true;
                                         di.LogSqlInConsole = true;
                                     });
        }
    }
}
