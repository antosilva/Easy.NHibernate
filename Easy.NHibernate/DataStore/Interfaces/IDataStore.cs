using System;
using Easy.NHibernate.Mappings.Interfaces;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session.Interfaces;

namespace Easy.NHibernate.DataStore.Interfaces
{
    public interface IDataStore : IModelMappings, ISessionManager, ISchemaExporter, IDisposable
    {
    }
}
