using System;
using Easy.NHibernate.Repository.Interfaces;
using NHibernate;

namespace Easy.NHibernate.Repository
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
            if (_transaction.IsActive)
            {
                _transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (_transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Rollback();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
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
