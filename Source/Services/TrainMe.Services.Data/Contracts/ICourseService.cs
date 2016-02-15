namespace TrainMe.Services.Data.Contracts
{
    using System.Collections.Generic;
    using TrainMe.Data.Models;

    public interface ICourseService
    {
        IEnumerable<Course> GetAll();

        IEnumerable<Course> GetTop(int count);
    }
}
