using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Easy.NHibernate.Mappings.Interfaces;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace Easy.NHibernate.Mappings
{
    public class ModelMappings : IModelMappings
    {
        protected readonly Configuration _configuration;
        protected readonly IList<Type> _mappings = new List<Type>();

        public ModelMappings(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void AddMappings(string exportingNamespace)
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain
                                               .GetAssemblies()
                                               .SelectMany(t => t.GetTypes())
                                               .Where(t => t.Namespace == exportingNamespace && t.IsClass);
            AddMappings(types);
        }

        public void AddMappings(Assembly exportingAssembly)
        {
            IEnumerable<Type> assemblyTypes = exportingAssembly.GetTypes();
            AddMappings(assemblyTypes);
        }

        public void AddMappings(IEnumerable<Assembly> exportingAssemblies)
        {
            IEnumerable<Type> assemblyTypes = exportingAssemblies.SelectMany(a => a.GetTypes());
            AddMappings(assemblyTypes);
        }

        public void AddMappings(Type mappingType)
        {
            if(typeof(IConformistHoldersProvider).IsAssignableFrom(mappingType))
            {
                _mappings.Add(mappingType);
            }
        }

        public void AddMappings(IEnumerable<Type> mappingTypes)
        {
            var types = mappingTypes as Type[] ?? mappingTypes.ToArray();
            IEnumerable<Type> mappingTypesOnly = types.Where(t => typeof(IConformistHoldersProvider).IsAssignableFrom(t));
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
    }
}
