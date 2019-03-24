namespace Czar.Cms.Core.Repository
{
    public interface IUnitOfWork
    {
        void Add<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        int Commit();
    }
}