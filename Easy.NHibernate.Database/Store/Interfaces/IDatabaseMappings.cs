using System;
using System.Collections.Generic;
using System.Reflection;

namespace Easy.NHibernate.Database.Store.Interfaces
{
    public interface IDatabaseMappings
    {
        void AddMappings(string exportingNamespace);
        void AddMappings(IEnumerable<Assembly> exportingAssemblies);
        void AddMappings(IEnumerable<Type> mappingTypes);

        void CompileMappings();
    }
}
