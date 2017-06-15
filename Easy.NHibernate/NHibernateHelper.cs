using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace Easy.NHibernate
{
    public class NHibernateHelper
    {
        private ISessionFactory _sessionFactory;
        private readonly Configuration _configuration;

        public NHibernateHelper(string configurationFile)
        {
            _configuration = new Configuration();
            _configuration.Configure(configurationFile);
        }

        public NHibernateHelper(Action<IDbIntegrationConfigurationProperties> databaseIntegration)
        {
            _configuration = new Configuration();
            _configuration.DataBaseIntegration(databaseIntegration);
        }

        public void AddMappingsFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            // Select only ClassMapping<> types.
            IEnumerable<Type> mappingTypes = assemblies.SelectMany(x => x.GetExportedTypes().Where(t => typeof(IConformistHoldersProvider).IsAssignableFrom(t)));
            AddMappings(mappingTypes);
        }

        public void AddMappings(IEnumerable<Type> types)
        {
            ModelMapper mapper = new ModelMapper();
            mapper.AddMappings(types.ToList());

            HbmMapping mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();
            _configuration.AddMapping(mappings);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
