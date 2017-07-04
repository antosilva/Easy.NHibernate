using System;
using Easy.NHibernate.Session.Interfaces;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Easy.NHibernate.Session
{
    public enum SessionContextAffinity
    {
        CurrentThread,
        Local,
        PerCall,
        WcfOperation,
        WebSession,
        CurrentDomain
    }

    public class SessionManager : ISessionManager
    {
        private readonly SessionContextAffinity _sessionContextAffinity;
        private readonly Lazy<ISessionFactory> _sessionFactory;

        public SessionManager(Configuration configuration)
            : this(configuration, SessionContextAffinity.CurrentThread)
        {
        }

        public SessionManager(Configuration configuration, SessionContextAffinity sessionContextAffinity)
        {
            _sessionContextAffinity = sessionContextAffinity;
            switch (_sessionContextAffinity)
            {
                case SessionContextAffinity.Local:
                    configuration.CurrentSessionContext<ThreadLocalSessionContext>();
                    break;
                case SessionContextAffinity.CurrentThread:
                    configuration.CurrentSessionContext<CurrentThreadSessionContext>();
                    break;
                case SessionContextAffinity.PerCall:
                    configuration.CurrentSessionContext<CallSessionContext>();
                    break;
                case SessionContextAffinity.WcfOperation:
                    configuration.CurrentSessionContext<WcfOperationSessionContext>();
                    break;
                case SessionContextAffinity.WebSession:
                    configuration.CurrentSessionContext<WebSessionContext>();
                    break;
                case SessionContextAffinity.CurrentDomain:
                    configuration.CurrentSessionContext<CurrentDomainSessionContext>();
                    break;
            }

            _sessionFactory = new Lazy<ISessionFactory>(configuration.BuildSessionFactory);
        }

        public ISession CurrentSession()
        {
            if (_sessionContextAffinity == SessionContextAffinity.Local || CurrentSessionContext.HasBind(_sessionFactory.Value))
            {
                return _sessionFactory.Value.GetCurrentSession();
            }

            ISession session = _sessionFactory.Value.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        public ISession UnbindCurrentSession()
        {
            if (_sessionContextAffinity == SessionContextAffinity.Local)
            {
                return ThreadLocalSessionContext.Unbind(_sessionFactory.Value);
            }

            return CurrentSessionContext.Unbind(_sessionFactory.Value);
        }
    }
}
