using System;
using System.Collections;
using System.Threading;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;

namespace Easy.NHibernate.Session
{
    /// <summary>
    /// Thread affinity session context.
    /// </summary>
    public class CurrentThreadSessionContext : CurrentSessionContext
    {
        protected static readonly ThreadLocal<ISession> _tlsSession = new ThreadLocal<ISession>();

        protected override ISession Session
        {
            get => _tlsSession.Value;
            set => _tlsSession.Value = value;
        }

        // Must exists, used by SessionFactoryImpl.BuildCurrentSessionContext
        public CurrentThreadSessionContext(ISessionFactoryImplementor factory)
        {
        }
    }
}
