namespace TrainMe.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ISomeModelService someModelService;

        public HomeController(ISomeModelService someModelService)
        {
            this.someModelService = someModelService;
        }

        public ActionResult Index()
        {
            var someViewModels = this.Mapper.Map<List<SomeModelViewModel>>(this.someModelService.GetTop(4));
            return this.View(someViewModels);
        }
    }
}
