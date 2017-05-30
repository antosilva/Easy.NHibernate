namespace Easy.NHibernate.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
