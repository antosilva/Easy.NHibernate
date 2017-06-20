using System;

namespace Easy.NHibernate.Database.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
