using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.NHibernate.Config;
using Easy.NHibernate.DataStore.Interfaces;
using Easy.NHibernate.Mapping;
using Easy.NHibernate.Mapping.Interfaces;
using Easy.NHibernate.Repository.Interfaces;
using Easy.NHibernate.Schema;
using Easy.NHibernate.Schema.Interfaces;
using Easy.NHibernate.Session;
using Easy.NHibernate.Session.Interfaces;
using Easy.NHibernate.UnitTests.AAA;
using Easy.NHibernate.UnitTests.Domain;
using Easy.NHibernate.UnitTests.Logger;
using Easy.NHibernate.UnitTests.Mappings;
using NHibernate.Cfg;

namespace Easy.NHibernate.UnitTests
{
    internal class SessionManagerTests : ArrangeActAssert
    {
        protected TestLogger Logger;
        protected SessionManager ObjectUnderTest;
        protected Configuration Configuration;

        public override void Arrange()
        {
            Logger = new TestLogger(@"logs\" + GetType() + ".log");
            Logger.Log.InfoFormat("Stating Unit tests");

            Configuration = new InMemoryConfiguration();
            Configuration.DataBaseIntegration(di => { di.LogSqlInConsole = false; });

            IModelMappings mappings = new ModelMappings(Configuration);
            mappings.AddMappings(typeof(CustomerMapping));
            mappings.CompileMappings();
        }
    }

    internal class SessionManagerTests_thread_local : SessionManagerTests
    {
        public override void Arrange()
        {
            base.Arrange();
            ObjectUnderTest = new SessionManager(Configuration, SessionContextAffinity.Threadlocal);
        }

        // TODO: to be continued
    }
}
