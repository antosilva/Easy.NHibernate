using System;
using Easy.NHibernate.Persistence.GenericRepository.Interfaces;
using NHibernate;

namespace Easy.NHibernate.Persistence.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransaction _transaction;

        public UnitOfWork(ISession session)
        {
            _transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction?.IsActive ?? false)
            {
                _transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (_transaction?.IsActive ?? false)
            {
                _transaction.Rollback();
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Rollback();
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}
