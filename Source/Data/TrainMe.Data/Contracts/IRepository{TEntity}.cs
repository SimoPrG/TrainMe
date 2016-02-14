namespace TrainMe.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TrainMe.Data.Models.Contracts;

    public interface IRepository<TEntity>
        where TEntity : IBaseModel
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);

        IEnumerable<TEntity> GetWithDeleted(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);

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
