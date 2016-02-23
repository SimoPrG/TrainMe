namespace TrainMe.Services.Data
{
    using System.Data.Entity;
    using System.Linq;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Base;
    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Infrastructure.Common;

    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Course> GetAll(string querry, string orderBy, int page, int pageSize)
        {
            var courses = this.QueryCourses(querry);

            switch (orderBy)
            {
                case QuerryStrings.CourseNameDesc:
                    courses = courses.OrderByDescending(c => c.Name);
                    break;
                case QuerryStrings.CourseCategoryName:
                    courses = courses.OrderBy(c => c.Category.Name);
                    break;
                case QuerryStrings.CourseCategoryNameDesc:
                    courses = courses.OrderByDescending(c => c.Category.Name);
                    break;
                case QuerryStrings.CourseAuthorName:
                    courses = courses.OrderBy(c => c.Author.UserName);
                    break;
                case QuerryStrings.CourseAuthorNameDesc:
                    courses = courses.OrderByDescending(c => c.Author.UserName);
                    break;
                default:
                    courses = courses.OrderBy(c => c.Name);
                    break;
            }

            return courses.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public int CountCourses(string querry)
        {
            return this.QueryCourses(querry).Count();
        }

        private IQueryable<Course> QueryCourses(string querry)
        {
            var courses = this.UnitOfWork.Courses.All()
                .Include(c => c.Category)
                .Include(c => c.Author);

            if (!string.IsNullOrWhiteSpace(querry))
            {
                courses = courses.Where(c => c.Name == querry || c.Category.Name == querry || c.Author.UserName == querry);
            }

            return courses;
        }
    }
}
