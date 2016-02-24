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

        public IQueryable<Course> All(string querry, int? categoryId, string orderBy, int page, int pageSize)
        {
            var courses = this.QueryCourses(querry, categoryId);

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

        public int CountCourses(string querry, int? categoryId)
        {
            return this.QueryCourses(querry, categoryId).Count();
        }

        public Course GetById(int id)
        {
            return this.UnitOfWork.Courses.GetById(id);
        }

        public void Add(Course course)
        {
            this.UnitOfWork.Courses.Add(course);
            this.UnitOfWork.Commit();
        }

        private IQueryable<Course> QueryCourses(string querry, int? categoryId)
        {
            var courses = this.UnitOfWork.Courses.All()
                .Include(c => c.Category)
                .Include(c => c.Author);

            if (!string.IsNullOrWhiteSpace(querry))
            {
                courses = courses.Where(c => c.Name.IndexOf(querry) != -1 || c.Author.UserName.IndexOf(querry) != -1);
            }

            if (categoryId.HasValue)
            {
                courses = courses.Where(c => c.CategoryId == categoryId.Value);
            }

            return courses;
        }
    }
}
