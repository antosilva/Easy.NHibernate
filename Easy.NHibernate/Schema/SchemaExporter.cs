using System.IO;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session.Interfaces;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Easy.NHibernate.Schema
{
    public class SchemaExporter : ISchemaExporter
    {
        private readonly ISessionManager _sessionManager;
        protected SchemaExport _schemaExport;

        public SchemaExporter(Configuration configuration, ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _schemaExport = new SchemaExport(configuration);
        }

        public void ExportToFile(string fileName)
        {
            _schemaExport.SetOutputFile(fileName).Execute(false /*stdout*/,
                                                          false /*execute*/,
                                                          false /*just drop*/);
        }

        public void ExportToConsole()
        {
            _schemaExport.Execute(true /*stdout*/,
                                  false /*execute*/,
                                  false /*just drop*/);
        }

        public string ExportToString()
        {
            StringWriter sw = new StringWriter();
            _schemaExport?.Execute(str => { },
                                   true /*execute*/,
                                   false /*just drop*/,
                                   sw);
            return sw.ToString();
        }

        public string ExportToDatabase()
        {
            StringWriter sw = new StringWriter();
            _schemaExport.Execute(false /*stdout*/,
                                  true /*execute*/,
                                  false /*just drop*/,
                                  _sessionManager.CurrentSession.Connection,
                                  sw);
            return sw.ToString();
        }
    }
}
