using System;
using NHibernate;

namespace Easy.NHibernate.Session.Interfaces
{
    public interface ISessionManager : IDisposable
    {
        ISession CurrentSession { get; }

        ISession UnbindCurrentSession();
    }
}
