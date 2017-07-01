using System;
using Easy.NHibernate.Session.Interfaces;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Easy.NHibernate.Session
{
    public class SessionManager : ISessionManager
    {
        private readonly Lazy<ISessionFactory> _sessionFactory;

        public SessionManager(Configuration configuration)
        {
            _sessionFactory = new Lazy<ISessionFactory>(configuration.BuildSessionFactory);
        }

        public ISession CurrentSession()
        {
            if (CurrentSessionContext.HasBind(_sessionFactory.Value))
            {
                return _sessionFactory.Value.GetCurrentSession();
            }

            ISession session = _sessionFactory.Value.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        public ISession UnbindCurrentSession()
        {
            return CurrentSessionContext.Unbind(_sessionFactory.Value);
        }
    }
}
