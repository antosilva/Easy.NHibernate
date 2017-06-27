using NHibernate;

namespace Easy.NHibernate.Database.Session.Interfaces
{
    public interface ISessionManager
    {
        ISession CurrentSession();
        ISession UnbindCurrentSession();
    }
}
