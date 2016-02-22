namespace TrainMe.Services.Data
{
    using System.Linq;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Base;
    using TrainMe.Services.Data.Contracts;

    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Category> All()
        {
            return this.UnitOfWork.Categories.All();
        }

        public void Add(Category category)
        {
            this.UnitOfWork.Categories.Add(category);
            this.UnitOfWork.Commit();
        }

        public Category GetById(int id)
        {
            return this.UnitOfWork.Categories.GetById(id);
        }

        public Category DeleteById(int id)
        {
            var category = this.UnitOfWork.Categories.GetById(id);
            this.UnitOfWork.FileDetails.Delete(category.ImageId);
            return this.UnitOfWork.Categories.Delete(id);
        }

        public IQueryable<Category> GetMostPopular(int count)
        {
            return this.All().OrderByDescending(c => c.Courses.Count).Take(count);
        }
    }
}
