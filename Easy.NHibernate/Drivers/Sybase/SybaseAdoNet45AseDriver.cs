using NHibernate.Driver;

namespace Easy.NHibernate.Drivers.Sybase
{
    public class SybaseAdoNet45AseDriver : ReflectionBasedDriver
    {
        public override string NamedPrefix => "@";
        public override bool UseNamedPrefixInParameter => true;
        public override bool UseNamedPrefixInSql => true;

        public SybaseAdoNet45AseDriver()
            : base("Sybase.AdoNet45.AseClient",
                   "Sybase.Data.AseClient.AseConnection",
                   "Sybase.Data.AseClient.AseCommand")
        {
        }
    }
}
