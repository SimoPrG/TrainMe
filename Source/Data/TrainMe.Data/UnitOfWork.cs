namespace TrainMe.Data
{
    using System;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITrainMeDbContext context;

        private bool isDisposed = false;

        public UnitOfWork(ITrainMeDbContext context, IRepository<User> users, IRepository<SomeModel> someModels)
        {
            this.context = context;
            this.Users = users;
            this.SomeModels = someModels;
        }

        public IRepository<User> Users { get; }

        public IRepository<SomeModel> SomeModels { get; }

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
