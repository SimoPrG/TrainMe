namespace TrainMe.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    public class HomeController : AdminController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
