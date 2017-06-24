using NHibernate;

namespace Easy.NHibernate.Database.Session.Interfaces
{
    public interface IDatabaseFacade
    {
        ISession CurrentSession();
    }
}
