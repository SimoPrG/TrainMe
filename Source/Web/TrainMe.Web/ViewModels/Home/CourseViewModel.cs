namespace TrainMe.Web.ViewModels.Home
{
    using AutoMapper;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string SomeProperty { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                .ForMember(d => d.Author, opts => opts.MapFrom(s => s.Author.UserName));
        }
    }
}
