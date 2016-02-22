namespace TrainMe.Data
{
    using System;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITrainMeDbContext context;

        private bool isDisposed = false;

        public UnitOfWork(
            ITrainMeDbContext context,
            IRepository<User> users,
            IRepository<Category> categories,
            IRepository<FileDetail> fileDetails,
            IRepository<Course> courses,
            IRepository<Lesson> lessons)
        {
            this.context = context;
            this.Users = users;
            this.Categories = categories;
            this.FileDetails = fileDetails;
            this.Courses = courses;
            this.Lessons = lessons;
        }

        public IRepository<User> Users { get; }

        public IRepository<Category> Categories { get; }

        public IRepository<FileDetail> FileDetails { get; }

        public IRepository<Course> Courses { get; }

        public IRepository<Lesson> Lessons { get; }

        public int Commit()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(this.context));
            }

            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.context.Dispose();
            }

            this.isDisposed = true;
        }
    }
}
