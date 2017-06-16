using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Easy.NHibernate.Database.Interfaces;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Util;

namespace Easy.NHibernate.Database
{
    public class DatabaseSession : IDatabaseSession
    {
        private ISessionFactory _sessionFactory;
        private readonly Configuration _configuration;
        private readonly IList<Type> _mappings = new List<Type>();

        public DatabaseSession(string configurationFile)
        {
            _configuration = new Configuration();
            _configuration.Configure(configurationFile);
        }

        public DatabaseSession(Action<IDbIntegrationConfigurationProperties> databaseIntegration)
        {
            _configuration = new Configuration();
            _configuration.DataBaseIntegration(databaseIntegration);
        }

        public void AddExportedMappingTypes(IEnumerable<Assembly> exportingAssemblies)
        {
            // Select only ClassMapping<> types.
            Type baseMappingType = typeof(IConformistHoldersProvider);
            IEnumerable<Type> mappingTypes = exportingAssemblies.SelectMany(a => a.GetExportedTypes().Where(t => baseMappingType.IsAssignableFrom(t)));
            AddMappingTypes(mappingTypes);
        }

        public void AddMappingTypes(IEnumerable<Type> types)
        {
            Type[] mappingTypes = types as Type[] ?? types.ToArray();
            mappingTypes.ForEach(_mappings.Add);
        }

        public ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                lock (_mappings)
                {
                    if (_sessionFactory == null)
                    {
                        ModelMapper mapper = new ModelMapper();
                        mapper.AddMappings(_mappings);
                        HbmMapping mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();
                        _configuration.AddMapping(mappings);

                        _sessionFactory = _configuration.BuildSessionFactory();
                    }
                }
            }
            return _sessionFactory.OpenSession();
        }
    }
}
