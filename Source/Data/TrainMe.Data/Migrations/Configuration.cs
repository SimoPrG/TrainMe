﻿namespace TrainMe.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TrainMe.Common;
    using TrainMe.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<TrainMeDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        // TODO: refactor
        protected override void Seed(TrainMeDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedCourses(context);
        }

        private void SeedRoles(TrainMeDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            foreach (var roleName in RoleNamesConstants.AllRoleNames)
            {
                if (!roleMgr.RoleExists(roleName))
                {
                    roleMgr.Create(new IdentityRole { Name = roleName });
                }
            }
        }

        private void SeedUsers(TrainMeDbContext context)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(context));

            // seed admin
            const string AdminEmail = "admin@train.me";
            var admin = new User
            {
                FirstName = "Admin",
                LastName = "Adminov",
                UserName = "Admin",
                Email = AdminEmail,
            };

            userMgr.Create(admin, "123456");

            if (!userMgr.IsInRole(userMgr.FindByEmail(AdminEmail).Id, RoleNamesConstants.AdministratorRoleName))
            {
                userMgr.AddToRole(userMgr.FindByEmail(AdminEmail).Id, RoleNamesConstants.AdministratorRoleName);
            }

            // seed user
            const string UserEmail = "user@train.me";
            var user = new User
            {
                FirstName = "User",
                LastName = "Userov",
                UserName = "User",
                Email = UserEmail
            };

            userMgr.Create(user, "123456");

            if (!userMgr.IsInRole(userMgr.FindByEmail(UserEmail).Id, RoleNamesConstants.UserRoleName))
            {
                userMgr.AddToRole(userMgr.FindByEmail(UserEmail).Id, RoleNamesConstants.UserRoleName);
            }
        }

        private void SeedCourses(TrainMeDbContext context)
        {
            var courses = context.Courses.ToArray();
            if (courses.Any())
            {
                return;
            }

            // TODO: seed
            context.Courses.AddOrUpdate(courses);
        }
    }
}
