namespace TrainMe.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using TrainMe.Web.Controllers;
    using TrainMe.Web.Infrastructure.Common;

    [Authorize(Roles = RoleNamesConstants.AdministratorRoleName)]
    public abstract class AdminController : BaseController
    {
    }
}
