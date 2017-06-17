using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;

namespace Easy.NHibernate.Database.Sessions.Interfaces
{
    public interface IDatabaseSessionFactory
    {
        void AddMappingTypes(IEnumerable<Assembly> exportingAssemblies);
        void AddMappingTypes(IEnumerable<Type> mappingTypes);

        void CompileMappings();

        ISession OpenSession();
    }
}
