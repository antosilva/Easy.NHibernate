using System;
using System.Data;
using System.Threading;
using Easy.NHibernate.Repository.Interfaces;
using NHibernate;

namespace Easy.NHibernate.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ITransaction _transaction;

        public UnitOfWork(ISession session)
        {
            _transaction = session.BeginTransaction();
        }

        public UnitOfWork(ISession session, IsolationLevel isolationLevel)
        {
            _transaction = session.BeginTransaction(isolationLevel);
        }

        public void Complete()
        {
            if (_transaction?.IsActive ?? false)
            {
                _transaction.Commit();
            }
        }

        protected void Rollback()
        {
            if (_transaction?.IsActive ?? false)
            {
                _transaction.Rollback();
            }
        }

        public void Dispose()
        {
            Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}
