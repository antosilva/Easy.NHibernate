using NHibernate.Dialect;
using NHibernate.Driver;

namespace Easy.NHibernate.Database.Session
{
    public class MsSqlSession : DatabaseSession
    {
        public MsSqlSession(string connectionString)
            : base(di =>
                   {
                       di.ConnectionString = connectionString;
                       di.Driver<Sql2008ClientDriver>();
                       di.Dialect<MsSql2012Dialect>();
                       di.LogFormattedSql = true;
                       di.LogSqlInConsole = true;
                   })
        {
            //_configuration.SetProperty(Environment.Dialect, typeof(MsSql2012Dialect).AssemblyQualifiedName);
            //_configuration.SetProperty(Environment.ConnectionDriver, typeof(Sql2008ClientDriver).AssemblyQualifiedName);
        }
    }
}
