namespace TrainMe.Services.Data
{
    using System.Linq;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Base;
    using TrainMe.Services.Data.Contracts;

    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Course> GetAll()
        {
            return this.UnitOfWork.Courses.All();
        }
    }
}
