using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Easy.NHibernate.Database.Schema
{
    public class SchemaExporter
    {
        protected SchemaExport _schemaExport;

        public SchemaExporter(Configuration configuration)
        {
            _schemaExport = new SchemaExport(configuration);
        }

        public void ExportToFile(string fileName)
        {
            _schemaExport.SetOutputFile(fileName).Execute(false /*stdout*/, false /*execute*/, false /*just drop*/);
        }

        public void ExportToConsole()
        {
            _schemaExport.Execute(true /*stdout*/, false /*execute*/, false /*just drop*/);
        }

        public void ExportToDatabase()
        {
            _schemaExport.Execute(false /*stdout*/, true /*execute*/, false /*just drop*/);
        }
    }
}
