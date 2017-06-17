using NHibernate.Driver;

namespace Easy.NHibernate.Database.Drivers.Sybase
{
    public class SybaseAdoNet4AseDriver : ReflectionBasedDriver
    {
        public override string NamedPrefix => "@";
        public override bool UseNamedPrefixInParameter => true;
        public override bool UseNamedPrefixInSql => true;

        public SybaseAdoNet4AseDriver()
            : base("Sybase.AdoNet4.AseClient",
                   "Sybase.Data.AseClient.AseConnection",
                   "Sybase.Data.AseClient.AseCommand")
        {
        }
    }
}
