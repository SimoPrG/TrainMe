namespace TrainMe.Services.Data.Contracts
{
    using System.Linq;
    using TrainMe.Data.Models;

    public interface ICourseService
    {
        IQueryable<Course> All(string querry, int? categoryId, string orderBy, int page, int pageSize);

        int CountCourses(string querry, int? categoryId);

        Course GetById(int id);

        void Add(Course course);

        void Update();
    }
}
