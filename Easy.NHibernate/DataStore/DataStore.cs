using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Easy.NHibernate.DataStore.Interfaces;
using Easy.NHibernate.Mapping.Interfaces;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session.Interfaces;
using NHibernate;

namespace Easy.NHibernate.DataStore
{
    public class DataStore : IDataStore
    {
        protected IModelMappings _modelMappings;
        protected ISessionManager _sessionManager;
        protected ISchemaExporter _schemaExport;
        protected int _disposed;

        public ISession CurrentSession => _sessionManager.CurrentSession;

        public DataStore(IModelMappings modelMappings, ISessionManager sessionManager, ISchemaExporter schemaExport)
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
            _schemaExport?.ExportToFile(fileName);
        }

        public void ExportToConsole()
        {
            _schemaExport?.ExportToConsole();
        }

        public string ExportToDatabase()
        {
            return _schemaExport?.ExportToDatabase();
        }

        public string ExportToString()
        {
            return _schemaExport?.ExportToString();
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
            if (Interlocked.Exchange(ref _disposed, 1) == 1)
            {
                return;
            }

            if (disposing)
            {
                _sessionManager?.Dispose();
                _sessionManager = null;
                _modelMappings = null;
                _schemaExport = null;
            }
        }

        ~DataStore()
        {
            Dispose(false);
        }
    }
}
