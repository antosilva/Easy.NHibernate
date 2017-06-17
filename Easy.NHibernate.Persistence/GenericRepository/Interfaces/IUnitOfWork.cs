using System;

namespace Easy.NHibernate.Persistence.GenericRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
