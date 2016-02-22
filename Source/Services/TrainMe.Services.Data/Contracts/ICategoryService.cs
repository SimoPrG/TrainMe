namespace TrainMe.Services.Data.Contracts
{
    using System.Linq;
    using TrainMe.Data.Models;

    public interface ICategoryService
    {
        IQueryable<Category> All();

        void Add(Category category);

        Category GetById(int id);

        void Update();

        Category DeleteById(int id);

        IQueryable<Category> GetMostPopular(int count);
    }
}
