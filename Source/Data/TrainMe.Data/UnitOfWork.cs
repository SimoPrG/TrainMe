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
            IRepository<SubCategory> subCategories,
            IRepository<FileDetail> fileDetails,
            IRepository<Course> courses)
        {
            this.context = context;
            this.Users = users;
            this.Categories = categories;
            this.SubCategories = subCategories;
            this.FileDetails = fileDetails;
            this.Courses = courses;
        }

        public IRepository<User> Users { get; }

        public IRepository<Category> Categories { get; }

        public IRepository<SubCategory> SubCategories { get; }

        public IRepository<FileDetail> FileDetails { get; }

        public IRepository<Course> Courses { get; }

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
