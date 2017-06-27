using System;
using System.Collections.Generic;
using System.Reflection;

namespace Easy.NHibernate.Database.Mappings.Interfaces
{
    public interface IModelMappings
    {
        void AddMappings(string exportingNamespace);

        void AddMappings(Assembly exportingAssembly);
        void AddMappings(IEnumerable<Assembly> exportingAssemblies);

        void AddMappings(Type mappingType);
        void AddMappings(IEnumerable<Type> mappingTypes);

        void CompileMappings();
    }
}
