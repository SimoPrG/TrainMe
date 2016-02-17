namespace TrainMe.Services.Data.Base
{
    using TrainMe.Data.Contracts;

    public abstract class BaseService
    {
        protected BaseService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public void Update()
        {
            this.UnitOfWork.Commit();
        }
    }
}
