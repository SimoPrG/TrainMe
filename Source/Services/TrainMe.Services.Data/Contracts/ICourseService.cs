namespace TrainMe.Services.Data.Contracts
{
    using System.Linq;
    using TrainMe.Data.Models;

    public interface ICourseService
    {
        IQueryable<Course> All(string querry, string category, string orderBy, int page, int pageSize);

        int CountCourses(string querry, string category);

        Course GetById(int id);
    }
}
