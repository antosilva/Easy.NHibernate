using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;

namespace Easy.NHibernate.Database.Interfaces
{
    public interface IDatabaseSession
    {
        void AddExportedMappingTypes(IEnumerable<Assembly> exportingAssemblies);
        void AddMappingTypes(IEnumerable<Type> mappingTypes);

        ISession OpenSession();
    }
}
