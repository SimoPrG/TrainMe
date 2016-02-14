namespace TrainMe.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using TrainMe.Data.Models;

    public interface ITrainMeDbContext : IDisposable
    {
        IDbSet<User> Users { get; set; }

        IDbSet<SomeModel> SomeModels { get; set; }

        IDbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();
    }
}
