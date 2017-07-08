using System;
using Easy.NHibernate.Session.Interfaces;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Easy.NHibernate.Session
{
    public enum SessionContextAffinity
    {
        ThreadLocal,    // One session per call.
        ThreadStatic,   // One session per thread.
        Call,           // One session per CallContext in remoting.
        WcfOperation,   // One session per OperationContext in wcf.
        Web             // One session per HttpContext, for web apps only.
    }

    public class SessionManager : ISessionManager
    {
        private readonly SessionContextAffinity _sessionContextAffinity;
        private Lazy<ISessionFactory> _sessionFactory;
        protected bool _disposed;

        public ISession CurrentSession => GetCurrentSession();

        public SessionManager(Configuration configuration)
            : this(configuration, SessionContextAffinity.ThreadStatic)
        {
        }

        public SessionManager(Configuration configuration, SessionContextAffinity sessionContextAffinity)
        {
            _sessionContextAffinity = sessionContextAffinity;
            switch (_sessionContextAffinity)
            {
                case SessionContextAffinity.ThreadLocal:
                    configuration.CurrentSessionContext<ThreadLocalSessionContext>();
                    break;
                case SessionContextAffinity.ThreadStatic:
                    configuration.CurrentSessionContext<ThreadStaticSessionContext>();
                    break;
                case SessionContextAffinity.Call:
                    configuration.CurrentSessionContext<CallSessionContext>();
                    break;
                case SessionContextAffinity.WcfOperation:
                    configuration.CurrentSessionContext<WcfOperationSessionContext>();
                    break;
                case SessionContextAffinity.Web:
                    configuration.CurrentSessionContext<WebSessionContext>();
                    break;
            }

            _sessionFactory = new Lazy<ISessionFactory>(configuration.BuildSessionFactory);
        }

        public ISession UnbindCurrentSession()
        {
            if (_sessionContextAffinity == SessionContextAffinity.ThreadLocal)
            {
                return ThreadLocalSessionContext.Unbind(_sessionFactory.Value);
            }

            return CurrentSessionContext.Unbind(_sessionFactory.Value);
        }

        private ISession GetCurrentSession()
        {
            if (_sessionContextAffinity == SessionContextAffinity.ThreadLocal || CurrentSessionContext.HasBind(_sessionFactory.Value))
            {
                return _sessionFactory.Value.GetCurrentSession();
            }

            ISession session = _sessionFactory.Value.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_sessionFactory.IsValueCreated)
                {
                    ISession session = UnbindCurrentSession();
                    session?.Dispose();
                    _sessionFactory.Value.Dispose();
                }
                _sessionFactory = null;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SessionManager()
        {
            Dispose(false);
        }
    }
}
