using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Easy.NHibernate.Database.Sessions.Interfaces;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace Easy.NHibernate.Database.Sessions
{
    public class DatabaseSessionFactory : IDatabaseSessionFactory
    {
        protected ISessionFactory _sessionFactory;
        protected readonly Configuration _configuration;
        protected readonly IList<Type> _mappings = new List<Type>();

        public DatabaseSessionFactory(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void AddMappingTypes(IEnumerable<Assembly> exportingAssemblies)
        {
            IEnumerable<Type> mappingTypes = exportingAssemblies.SelectMany(a => ExtractMappingTypesFrom(a.GetExportedTypes()));
            foreach (Type mappingType in mappingTypes)
            {
                _mappings.Add(mappingType);
            }
        }

        public void AddMappingTypes(IEnumerable<Type> mappingTypes)
        {
            var types = mappingTypes as Type[] ?? mappingTypes.ToArray();
            var mappingTypesOnly = ExtractMappingTypesFrom(types);
            foreach (Type mappingType in mappingTypesOnly)
            {
                _mappings.Add(mappingType);
            }
        }

        public void CompileMappings()
        {
            ModelMapper mapper = new ModelMapper();
            mapper.AddMappings(_mappings);
            HbmMapping mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();
            _configuration.AddMapping(mappings);
        }

        public ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                lock (_mappings)
                {
                    if (_sessionFactory == null)
                    {
                        _sessionFactory = _configuration.BuildSessionFactory();
                    }
                }
            }
            return _sessionFactory.OpenSession();
        }

        protected IEnumerable<Type> ExtractMappingTypesFrom(IEnumerable<Type> types)
        {
            IEnumerable<Type> mappingTypesOnly = types.Where(t => typeof(IConformistHoldersProvider).IsAssignableFrom(t));
            return mappingTypesOnly;
        }
    }
}
