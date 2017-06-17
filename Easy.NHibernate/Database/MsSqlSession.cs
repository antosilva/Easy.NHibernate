using NHibernate.Dialect;
using NHibernate.Driver;

namespace Easy.NHibernate.Database
{
    public class MsSqlSession : DatabaseSession
    {
        public MsSqlSession(string connectionString)
            : base(di =>
                   {
                       di.ConnectionString = connectionString;
                       di.Driver<Sql2008ClientDriver>();
                       di.Dialect<MsSql2012Dialect>();
                   })
        {
            //_configuration.SetProperty(Environment.Dialect, typeof(MsSql2012Dialect).AssemblyQualifiedName);
            //_configuration.SetProperty(Environment.ConnectionDriver, typeof(Sql2008ClientDriver).AssemblyQualifiedName);
        }
    }
}
