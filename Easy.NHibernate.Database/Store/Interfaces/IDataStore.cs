using System;
using Easy.NHibernate.Database.Mappings.Interfaces;
using Easy.NHibernate.Database.Schema.Interfaces;
using Easy.NHibernate.Database.Session.Interfaces;

namespace Easy.NHibernate.Database.Store.Interfaces
{
    public interface IDataStore : IModelMappings, ISessionManager, ISchemaExporter, IDisposable
    {
    }
}
