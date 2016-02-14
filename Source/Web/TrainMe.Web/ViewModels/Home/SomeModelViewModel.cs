namespace TrainMe.Web.ViewModels.Home
{
    using AutoMapper;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class SomeModelViewModel : IMapFrom<SomeModel>, IHaveCustomMappings
    {
        public string SomeProperty { get; set; }

        public string Owner { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<SomeModel, SomeModelViewModel>()
                .ForMember(d => d.Owner, opts => opts.MapFrom(s => s.User.UserName));
        }
    }
}
