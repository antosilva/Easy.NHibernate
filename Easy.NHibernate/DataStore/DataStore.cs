using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Easy.NHibernate.DataStore.Interfaces;
using Easy.NHibernate.Mappings.Interfaces;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session.Interfaces;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Easy.NHibernate.DataStore
{
    public class DataStore : IDataStore
    {
        private IModelMappings _modelMappings;
        private ISessionManager _sessionManager;
        private readonly SchemaExport _schemaExport;

        public DataStore(IModelMappings modelMappings, ISessionManager sessionManager, SchemaExport schemaExport)
        {
            _modelMappings = modelMappings;
            _sessionManager = sessionManager;
            _schemaExport = schemaExport;
        }

        public DataStore(IModelMappings modelMappings, ISessionManager sessionManager)
            : this(modelMappings, sessionManager, null)
        {
        }

        public void AddMappings(string exportingNamespace)
        {
            _modelMappings.AddMappings(exportingNamespace);
        }

        public void AddMappings(Assembly exportingAssembly)
        {
            _modelMappings.AddMappings(exportingAssembly);
        }

        public void AddMappings(IEnumerable<Assembly> exportingAssemblies)
        {
            _modelMappings.AddMappings(exportingAssemblies);
        }

        public void AddMappings(Type mappingType)
        {
            _modelMappings.AddMappings(mappingType);
        }

        public void AddMappings(IEnumerable<Type> mappingTypes)
        {
            _modelMappings.AddMappings(mappingTypes);
        }

        public void CompileMappings()
        {
            _modelMappings.CompileMappings();
        }

        public void ExportToFile(string fileName)
        {
            _schemaExport?.SetOutputFile(fileName).Execute(false /*stdout*/,
                                                           false /*execute*/,
                                                           false /*just drop*/);
        }

        public void ExportToConsole()
        {
            _schemaExport?.Execute(true /*stdout*/, false /*execute*/, false /*just drop*/);
        }

        public string ExportToDatabase()
        {
            StringWriter sw = new StringWriter();
            _schemaExport?.Execute(false /*stdout*/,
                                   true /*execute*/,
                                   false /*just drop*/,
                                   _sessionManager.CurrentSession().Connection,
                                   sw);
            return sw.ToString();
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

        public ISession CurrentSession()
        {
            return _sessionManager.CurrentSession();
        }

        public ISession UnbindCurrentSession()
        {
            return _sessionManager.UnbindCurrentSession();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                ISession currentSession = _sessionManager?.UnbindCurrentSession();
                currentSession?.Dispose();

                _modelMappings = null;
                _sessionManager = null;
            }
        }

        ~DataStore()
        {
            Dispose(false);
        }
    }
}
