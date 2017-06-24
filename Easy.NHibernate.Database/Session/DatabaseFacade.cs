using Easy.NHibernate.Database.Session.Interfaces;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Easy.NHibernate.Database.Session
{
    public class DatabaseFacade : IDatabaseFacade
    {
        private readonly ISessionFactory _sessionFactory;

        public DatabaseFacade(Configuration configuration)
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

        public void Unbind()
        {
            CurrentSessionContext.Unbind(_sessionFactory);
        }
    }
}
