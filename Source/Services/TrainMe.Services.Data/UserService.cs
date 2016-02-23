namespace TrainMe.Services.Data
{
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Base;
    using TrainMe.Services.Data.Contracts;

    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public User GetById(string id)
        {
            return this.UnitOfWork.Users.GetById(id);
        }
    }
}
