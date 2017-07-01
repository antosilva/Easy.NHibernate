using System;
using System.Data;
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

        public UnitOfWork(ISession session, IsolationLevel isolationLevel)
        {
            _transaction = session.BeginTransaction(isolationLevel);
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
    }
}
