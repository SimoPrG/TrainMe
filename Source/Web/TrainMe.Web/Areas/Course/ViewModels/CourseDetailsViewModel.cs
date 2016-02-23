namespace TrainMe.Web.Areas.Course.ViewModels
{
    using System.ComponentModel;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CourseDetailsViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; }

        [DisplayName("Trainer")]
        public string AuthorUserName { get; set; }

        [DisplayName("Number of lessons")]
        public int LessonsCount { get; set; }

        [DisplayName("Number of attendees")]
        public int AttendeesCount { get; set; }
    }
}
