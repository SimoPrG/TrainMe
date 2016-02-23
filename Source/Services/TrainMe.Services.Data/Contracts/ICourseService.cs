namespace TrainMe.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using TrainMe.Data.Models;

    public interface ICourseService
    {
        IQueryable<Course> GetAll(string querry, string orderBy, int page, int pageSize);

        int CountCourses(string querry);
    }
}
