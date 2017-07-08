using System;
using System.Collections.Generic;
using System.Reflection;
using Easy.NHibernate.DataStore.Interfaces;
using Easy.NHibernate.Mapping.Interfaces;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session.Interfaces;
using NHibernate;

namespace Easy.NHibernate.DataStore
{
    public class DataStore : IDataStore
    {
        private IModelMappings _modelMappings;
        private ISessionManager _sessionManager;
        private ISchemaExporter _schemaExport;
        protected bool _disposed;

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
            if (_disposed)
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

            _disposed = true;
        }

        ~DataStore()
        {
            Dispose(false);
        }
    }
}
