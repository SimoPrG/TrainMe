namespace TrainMe.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
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

        public virtual IQueryable<TEntity> All()
        {
            return this.DbSet.Where(e => !e.IsDeleted);
        }

        public virtual IQueryable<TEntity> AllWithDeleted()
        {
            return this.DbSet;
        }

        public virtual TEntity GetById(object id)
        {
            var entity = this.GetByIdWithDeleted(id);
            if (entity == null || entity.IsDeleted)
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
