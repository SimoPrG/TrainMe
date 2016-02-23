namespace TrainMe.Web.Infrastructure.Common
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class RoleNamesConstants
    {
        public const string AdministratorRoleName = "administrator";
        public const string TrainerRoleName = "trainer";
        public const string UserRoleName = "user";

        public static readonly IReadOnlyCollection<string> AllRoleNames =
            new ReadOnlyCollection<string>(new []{ AdministratorRoleName, TrainerRoleName, UserRoleName });
    }
}
