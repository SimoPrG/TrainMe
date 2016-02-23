namespace TrainMe.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;
    using TrainMe.Services.Web;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();

        [ChildActionOnly]
        public ActionResult RenderWarning(string tempDataKey)
        {
            if (this.TempData[tempDataKey] != null)
            {
                return this.PartialView("_WarningPartial", tempDataKey);
            }

            return new EmptyResult();
        }
    }
}
