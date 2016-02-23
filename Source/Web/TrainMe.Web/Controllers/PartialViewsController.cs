namespace TrainMe.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Areas.Course.InputModels.Course;
    using TrainMe.Web.Infrastructure.Common;
    using WebCommon = TrainMe.Web.Common;

    [ChildActionOnly]
    public class PartialViewsController : BaseController
    {
        private readonly IUserService userService;

        public PartialViewsController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult RenderWarning(string tempDataKey)
        {
            if (this.TempData[tempDataKey] != null)
            {
                return this.PartialView("_WarningPartial", tempDataKey);
            }

            return new EmptyResult();
        }

        public ActionResult RenderCreateLink(object htmlAtributes)
        {
            if (this.User.IsInRole(RoleNamesConstants.TrainerRoleName))
            {
                return this.PartialView("_CreateLinkPartial", htmlAtributes);
            }

            return new EmptyResult();
        }

        public ActionResult RenderEnroll()
        {
            if (this.Request.IsAuthenticated)
            {
                var course = (Course)this.HttpContext.Items[WebCommon.Constants.HttpRequestItemsCourseKey];
                var user = this.userService.GetById(this.User.Identity.GetUserId());
                if (course.Author != user && !course.Attendees.Contains(user))
                {
                    return this.PartialView("_EnrollPartial", new CourseEnrollInputModel { CourseId = course.Id });
                }

                return new EmptyResult();
            }

            return this.PartialView("_RegisterOrLoginToProceedPartial");
        }

        public ActionResult RenderLogInRegisterOrLogOff()
        {
            if (this.Request.IsAuthenticated)
            {
                return this.PartialView("_LogOffPartial");
            }

            return this.PartialView("_LoginRegisterPartial");
        }

        public ActionResult RenderAdminMenu()
        {
            if (this.User.IsInRole(RoleNamesConstants.AdministratorRoleName))
            {
                return this.PartialView("_AdminDropDownMenuPartial");
            }

            return new EmptyResult();
        }
    }
}
