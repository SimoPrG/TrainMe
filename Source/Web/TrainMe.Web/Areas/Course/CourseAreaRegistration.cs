namespace TrainMe.Web.Areas.Course
{
    using System.Web.Mvc;

    public class CourseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Course"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Course_default",
                url: "Course/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TrainMe.Web.Areas.Course.Controllers" });
        }
    }
}
