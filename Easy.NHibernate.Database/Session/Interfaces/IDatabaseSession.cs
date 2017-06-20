using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;

namespace Easy.NHibernate.Database.Session.Interfaces
{
    public interface IDatabaseSession
    {
        void AddMappingTypes(string exportingNamespace);
        void AddMappingTypes(IEnumerable<Assembly> exportingAssemblies);
        void AddMappingTypes(IEnumerable<Type> mappingTypes);

        void CompileMappings();

        ISession OpenSession();
    }
}
