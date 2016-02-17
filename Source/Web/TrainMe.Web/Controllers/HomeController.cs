namespace TrainMe.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Infrastructure.Mapping;
    using TrainMe.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ICourseService courseService;

        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public ActionResult Index()
        {
            var someViewModels = this.courseService.GetAll().To<CourseViewModel>();
            return this.View(someViewModels);
        }
    }
}
