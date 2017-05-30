using Easy.NHibernate.Repository.Interfaces;
using NHibernate;

namespace Easy.NHibernate.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITransaction _transaction;

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
    }
}
