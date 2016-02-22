namespace TrainMe.Web.Controllers
{
    using System.Web.Mvc;

    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Infrastructure.Mapping;
    using TrainMe.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            const int NumberOfCategoriesToDisplay = 8;
            var mostPopularCategories = this.categoryService.GetMostPopular(NumberOfCategoriesToDisplay).To<CategoryViewModel>();
            return this.View(mostPopularCategories);
        }
    }
}
