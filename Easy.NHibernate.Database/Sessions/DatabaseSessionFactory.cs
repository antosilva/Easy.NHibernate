﻿using System;
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
        protected readonly Lazy<ISessionFactory> _sessionFactory;
        protected readonly Configuration _configuration;
        protected readonly IList<Type> _mappings;

        public DatabaseSessionFactory(Configuration configuration)
        {
            _configuration = configuration;
            _mappings = new List<Type>();
            _sessionFactory = new Lazy<ISessionFactory>(() => _configuration.BuildSessionFactory());
        }

        public void AddMappingTypes(string exportingNamespace)
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain
                                               .GetAssemblies()
                                               .SelectMany(t => t.GetExportedTypes())
                                               .Where(t => t.Namespace == exportingNamespace && t.IsClass);
            AddMappingTypes(types);
        }

        public void AddMappingTypes(IEnumerable<Assembly> exportingAssemblies)
        {
            IEnumerable<Type> exportedTypes = exportingAssemblies.SelectMany(a => a.GetExportedTypes());
            AddMappingTypes(exportedTypes);
        }

        public void AddMappingTypes(IEnumerable<Type> mappingTypes)
        {
            var types = mappingTypes as Type[] ?? mappingTypes.ToArray();
            var mappingTypesOnly = GetMappingTypesFrom(types);
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
            return _sessionFactory.Value.OpenSession();
        }

        protected IEnumerable<Type> GetMappingTypesFrom(IEnumerable<Type> types)
        {
            IEnumerable<Type> mappingTypesOnly = types.Where(t => typeof(IConformistHoldersProvider).IsAssignableFrom(t));
            return mappingTypesOnly;
        }
    }
}
