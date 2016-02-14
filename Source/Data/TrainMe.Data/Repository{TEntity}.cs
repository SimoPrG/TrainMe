namespace TrainMe.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models.Contracts;

    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseModel
    {
        private readonly ITrainMeDbContext context;

        public Repository(ITrainMeDbContext context)
        {
            this.context = context;
            this.DbSet = this.context.Set<TEntity>();
        }

        protected virtual ITrainMeDbContext Context => this.context;

        protected virtual IDbSet<TEntity> DbSet { get; }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> include = null,
            int? page = null,
            int? pageSize = null)
        {
            Expression<Func<TEntity, bool>> expression = x => !x.IsDeleted;

            if (filter != null)
            {
                var body = Expression.AndAlso(expression.Body, filter.Body);
                expression = Expression.Lambda<Func<TEntity, bool>>(body, expression.Parameters);
            }

            return this.GetWithDeleted(expression, orderBy, include, page, pageSize);
        }

        public virtual IEnumerable<TEntity> GetWithDeleted(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> include = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = this.DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                foreach (var expression in include)
                {
                    query = query.Include(expression);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != null && pageSize != null)
            {
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            var entity = this.GetByIdWithDeleted(id);
            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }

        public virtual TEntity GetByIdWithDeleted(object id)
        {
            return this.DbSet.Find(id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            if (!entity.IsDeleted)
            {
                entity.IsDeleted = true;
                entity.DeletedOn = DateTime.UtcNow;
            }

            return this.Update(entity);
        }

        public virtual TEntity Delete(object id)
        {
            return this.Delete(this.GetById(id));
        }

        public virtual TEntity HardDelete(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public virtual TEntity HardDelete(object id)
        {
            var entity = this.DbSet.Find(id);
            return this.HardDelete(entity);
        }

        public virtual TEntity Attach(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Unchanged;
            return entity;
        }

        public virtual TEntity Detach(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}
