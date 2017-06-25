using Easy.NHibernate.Database.Store.Interfaces;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Easy.NHibernate.Database.Store
{
    public class DatabaseSession : IDatabaseSession
    {
        private readonly ISessionFactory _sessionFactory;

        public DatabaseSession(Configuration configuration)
        {
            _sessionFactory = configuration.BuildSessionFactory();
        }

        public ISession CurrentSession()
        {
            if (CurrentSessionContext.HasBind(_sessionFactory))
            {
                return _sessionFactory.GetCurrentSession();
            }

            ISession session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        public ISession UnbindCurrentSession()
        {
            return CurrentSessionContext.Unbind(_sessionFactory);
        }
    }
}
