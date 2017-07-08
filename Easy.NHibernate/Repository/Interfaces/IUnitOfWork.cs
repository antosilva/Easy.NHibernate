using System;

namespace Easy.NHibernate.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
    }
}
