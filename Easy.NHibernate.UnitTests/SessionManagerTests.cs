using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Easy.NHibernate.Config;
using Easy.NHibernate.Mapping;
using Easy.NHibernate.Mapping.Interfaces;
using Easy.NHibernate.Session;
using Easy.NHibernate.Session.Interfaces;
using Easy.NHibernate.UnitTests.AAA;
using Easy.NHibernate.UnitTests.Logger;
using Easy.NHibernate.UnitTests.Mappings;
using FluentAssertions;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace Easy.NHibernate.UnitTests
{
    internal class SessionManagerTests : ArrangeActAssert
    {
        protected TestLogger Logger;
        protected Configuration Configuration;
        protected IList<int> HashCodes;
        protected ISessionManager ObjectUnderTest1;
        protected ISessionManager ObjectUnderTest2;

        public override void Arrange()
        {
            Logger = new TestLogger(@"logs\" + GetType() + ".log");
            Logger.Log.InfoFormat("Stating Unit tests");

            Configuration = new InMemoryConfiguration();
            Configuration.DataBaseIntegration(di => { di.LogSqlInConsole = false; });

            IModelMappings mappings = new ModelMappings(Configuration);
            mappings.AddMappings(typeof(CustomerMapping));
            mappings.CompileMappings();

            HashCodes = new List<int>();

            InitManagers();
        }

        public override void Act()
        {
            Action action = () =>
                            {
                                ISession session1 = ObjectUnderTest1.CurrentSession;
                                HashCodes.Add(session1.GetHashCode());
                                ISession session2 = ObjectUnderTest2.CurrentSession;
                                HashCodes.Add(session2.GetHashCode());
                            };

            Parallel.Invoke(action, action, action, action);
            while (HashCodes.Count != 8)
            {
                Thread.Sleep(100);
            }
        }

        protected virtual void InitManagers()
        {
        }
    }

    internal class SessionManagerTests_per_call_session : SessionManagerTests
    {
        protected override void InitManagers()
        {
            ObjectUnderTest1 = new SessionManager(Configuration, SessionContextAffinity.Call);
            ObjectUnderTest2 = new SessionManager(Configuration, SessionContextAffinity.Call);
        }

        [Test]
        public void Assert_should_creates_one_session_for_each_call()
        {
            HashCodes.Distinct().Count().Should().Be(8);
        }
    }

    internal class SessionManagerTests_thread_local : SessionManagerTests
    {
        protected override void InitManagers()
        {
            ObjectUnderTest1 = new SessionManager(Configuration, SessionContextAffinity.ThreadLocal);
            ObjectUnderTest2 = new SessionManager(Configuration, SessionContextAffinity.ThreadLocal);
        }

        [Test]
        public void Assert_should_creates_one_session_for_each_call()
        {
            HashCodes.Distinct().Count().Should().Be(8);
        }
    }

    internal class SessionManagerTests_thread_static : SessionManagerTests
    {
        protected override void InitManagers()
        {
            ObjectUnderTest1 = new SessionManager(Configuration, SessionContextAffinity.ThreadStatic);
            ObjectUnderTest2 = new SessionManager(Configuration, SessionContextAffinity.ThreadStatic);
        }

        [Test]
        public void Assert_should_create_one_session_per_thread()
        {
            HashCodes.Distinct().Count().Should().Be(4);
        }
    }
}
