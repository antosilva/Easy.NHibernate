using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate.Cfg;

namespace Easy.NHibernate.Database.Session.Interfaces
{
    public interface IDatabaseMappings
    {
        void AddMappings(string exportingNamespace);
        void AddMappings(IEnumerable<Assembly> exportingAssemblies);
        void AddMappings(IEnumerable<Type> mappingTypes);

        void CompileMappings();
    }
}
