using NHibernate;

namespace Easy.NHibernate.Session.Interfaces
{
    public interface ISessionManager
    {
        ISession CurrentSession();
        ISession UnbindCurrentSession();
    }
}
