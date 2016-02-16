namespace TrainMe.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using TrainMe.Common;
    using TrainMe.Web.Controllers;

    [Authorize(Roles = RoleNamesConstants.AdministratorRoleName)]
    public abstract class AdminController : BaseController
    {
    }
}
