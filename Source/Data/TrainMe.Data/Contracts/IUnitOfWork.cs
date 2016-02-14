namespace TrainMe.Data.Contracts
{
    using System;
    using TrainMe.Data.Models;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }

        IRepository<SomeModel> SomeModels { get; }

        int Commit();
    }
}
