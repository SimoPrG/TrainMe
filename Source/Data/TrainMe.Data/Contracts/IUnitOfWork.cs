namespace TrainMe.Data.Contracts
{
    using System;
    using TrainMe.Data.Models;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<SubCategory> SubCategories { get; }

        IRepository<Course> Courses { get; }

        int Commit();
    }
}
