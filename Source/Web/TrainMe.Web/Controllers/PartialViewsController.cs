namespace TrainMe.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Areas.Course.InputModels.Course;
    using TrainMe.Web.Common;
    using TrainMe.Web.Infrastructure.Common;

    [ChildActionOnly]
    public class PartialViewsController : BaseController
    {
        private readonly IUserService userService;

        public PartialViewsController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult RenderSuccess()
        {
            if (this.TempData[TempDataKeys.Success] != null)
            {
                return this.PartialView("_SuccessPartial");
            }

            return new EmptyResult();
        }

        public ActionResult RenderInfo()
        {
            if (this.TempData[TempDataKeys.Info] != null)
            {
                return this.PartialView("_InfoPartial");
            }

            return new EmptyResult();
        }

        public ActionResult RenderWarning()
        {
            if (this.TempData[TempDataKeys.Warning] != null)
            {
                return this.PartialView("_WarningPartial");
            }

            return new EmptyResult();
        }

        public ActionResult RenderDanger()
        {
            if (this.TempData[TempDataKeys.Danger] != null)
            {
                return this.PartialView("_DangerPartial");
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

        public ActionResult RenderCourseEditAndDeleteButtons()
        {
            var course = (Course)this.HttpContext.Items[WebConstants.HttpRequestItemsCourseKey];
            if (course.AuthorId == this.User.Identity.GetUserId())
            {
                return this.PartialView("_CourseEditAndDeleteButtons", new CourseDeleteInputModel { CourseId = course.Id });
            }

            return new EmptyResult();
        }

        public ActionResult RenderEnroll()
        {
            if (this.Request.IsAuthenticated)
            {
                var course = (Course)this.HttpContext.Items[WebConstants.HttpRequestItemsCourseKey];
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
