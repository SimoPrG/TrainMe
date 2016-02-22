namespace TrainMe.Web.ViewModels.Home
{
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string ImageFileName { get; set; }
    }
}
