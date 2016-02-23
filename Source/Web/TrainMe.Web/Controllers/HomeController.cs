namespace TrainMe.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
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

            var mostPopularCategories = this.Cache.Get<IEnumerable<CategoryViewModel>>(
                "popularCategories",
                () => this.categoryService.GetMostPopular(NumberOfCategoriesToDisplay).To<CategoryViewModel>().ToList(),
                2 * 60 * 60);

            return this.View(mostPopularCategories);
        }
    }
}
