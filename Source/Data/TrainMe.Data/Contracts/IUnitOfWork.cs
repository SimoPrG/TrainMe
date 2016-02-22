namespace TrainMe.Data.Contracts
{
    using System;
    using TrainMe.Data.Models;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<FileDetail> FileDetails { get; }

        IRepository<Course> Courses { get; }

        IRepository<Lesson> Lessons { get; }

        int Commit();
    }
}
