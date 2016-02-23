namespace TrainMe.Web.Areas.Course.InputModels.Course
{
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CourseEnrollInputModel : IMapTo<Course>
    {
        public int Id { get; set; }
    }
}