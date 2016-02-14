namespace TrainMe.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;
    using TrainMe.Data.Models.Contracts;

    public class TrainMeDbContext : IdentityDbContext<User>, ITrainMeDbContext
    {
        public TrainMeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<SomeModel> SomeModels { get; set; }

        public static TrainMeDbContext Create()
        {
            return new TrainMeDbContext();
        }

        IDbSet<TEntity> ITrainMeDbContext.Set<TEntity>()
        {
            return this.Set<TEntity>();
        }

        DbEntityEntry<TEntity> ITrainMeDbContext.Entry<TEntity>(TEntity entity)
        {
            return this.Entry(entity);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected virtual void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
