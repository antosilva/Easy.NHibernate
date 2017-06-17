using NHibernate.Dialect;
using Easy.NHibernate.Drivers.Sybase;

namespace Easy.NHibernate.Database
{
    public class SybaseAseSession : DatabaseSession
    {
        public SybaseAseSession(string connectionString)
            : base(di =>
                   {
                       di.ConnectionString = connectionString;
                       di.Dialect<SybaseASE15Dialect>();
                       di.Driver<SybaseAdoNet4AseDriver>();
                   })
        {
            //_configuration.SetProperty(Environment.Dialect, typeof(SybaseASE15Dialect).AssemblyQualifiedName);
            //_configuration.SetProperty(Environment.ConnectionDriver, typeof(SybaseAdoNet4AseDriver).AssemblyQualifiedName);
        }
    }
}
