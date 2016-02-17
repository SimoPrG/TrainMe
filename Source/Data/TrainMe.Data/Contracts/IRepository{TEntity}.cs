namespace TrainMe.Data.Contracts
{
    using System.Linq;
    using TrainMe.Data.Models.Contracts;

    public interface IRepository<TEntity>
        where TEntity : IBaseModel
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllWithDeleted();

        TEntity GetById(object id);

        TEntity GetByIdWithDeleted(object id);

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Delete(TEntity entity);

        TEntity Delete(object id);

        TEntity HardDelete(TEntity entity);

        TEntity HardDelete(object id);

        TEntity Attach(TEntity entity);

        TEntity Detach(TEntity entity);
    }
}
