using NHibernate;

namespace Easy.NHibernate.Database.Store.Interfaces
{
    public interface IDatabaseSession
    {
        ISession CurrentSession();
        ISession UnbindCurrentSession();
    }
}
