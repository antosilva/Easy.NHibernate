using System;
using Easy.NHibernate.Database.Session.Interfaces;
using Easy.NHibernate.Database.Store.Interfaces;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Easy.NHibernate.Database.Session
{
    public class CurrentSessionContextManager : ISessionManager
    {
        private readonly Lazy<ISessionFactory> _sessionFactory;

        public CurrentSessionContextManager(Configuration configuration)
        {
            configuration.CurrentSessionContext<ThreadStaticSessionContext>();
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
