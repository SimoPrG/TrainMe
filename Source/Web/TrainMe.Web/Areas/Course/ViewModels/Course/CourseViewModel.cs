namespace TrainMe.Web.Areas.Course.ViewModels.Course
{
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string AuthorUserName { get; set; }
    }
}