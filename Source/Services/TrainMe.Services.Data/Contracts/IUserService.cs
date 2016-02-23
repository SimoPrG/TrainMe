namespace TrainMe.Services.Data.Contracts
{
    using TrainMe.Data.Models;

    public interface IUserService
    {
        User GetById(string id);
    }
}
