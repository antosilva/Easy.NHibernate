using System;
using System.Collections.Generic;
using System.Reflection;
using Easy.NHibernate.Database.Mappings.Interfaces;
using Easy.NHibernate.Database.Schema.Interfaces;
using Easy.NHibernate.Database.Session.Interfaces;
using Easy.NHibernate.Database.Store.Interfaces;
using NHibernate;

namespace Easy.NHibernate.Database.Store
{
    public class DataStore : IDataStore
    {
        private IModelMappings _modelMappings;
        private ISessionManager _sessionManager;
        private ISchemaExporter _schemaExporter;

        public DataStore(IModelMappings modelMappings, ISessionManager sessionManager, ISchemaExporter schemaExporter)
        {
            _modelMappings = modelMappings;
            _sessionManager = sessionManager;
            _schemaExporter = schemaExporter;
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

        public ISession CurrentSession()
        {
            return _sessionManager.CurrentSession();
        }

        public ISession UnbindCurrentSession()
        {
            return _sessionManager.UnbindCurrentSession();
        }

        public void ExportToFile(string fileName)
        {
            _schemaExporter.ExportToFile(fileName);
        }

        public void ExportToConsole()
        {
            _schemaExporter.ExportToConsole();
        }

        public void ExportToDatabase()
        {
            _schemaExporter.ExportToDatabase();
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
                _schemaExporter = null;
            }
        }

        ~DataStore()
        {
            Dispose(false);
        }
    }
}
