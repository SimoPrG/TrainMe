namespace TrainMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Common;

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

            // this.SeedFileDetails(context);
            // this.SeedCategories(context);
            this.SeedCourses(context);
        }

        private void SeedRoles(TrainMeDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

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
            if (context.Users.Any())
            {
                return;
            }

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

            // seed users
            for (int i = 1; i <= 12; i++)
            {
                string userEmail = $"user{i}@train.me";
                var user = new User
                {
                    FirstName = $"User{i}",
                    LastName = $"Userov{i}",
                    UserName = $"User{i}",
                    Email = userEmail
                };

                userMgr.Create(user, "123456");

                if (!userMgr.IsInRole(userMgr.FindByEmail(userEmail).Id, RoleNamesConstants.UserRoleName))
                {
                    userMgr.AddToRole(userMgr.FindByEmail(userEmail).Id, RoleNamesConstants.UserRoleName);
                }

                if (i % 3 == 0 && !userMgr.IsInRole(userMgr.FindByEmail(userEmail).Id, RoleNamesConstants.TrainerRoleName))
                {
                    userMgr.AddToRole(userMgr.FindByEmail(userEmail).Id, RoleNamesConstants.TrainerRoleName);
                }
            }
        }

        private void SeedFileDetails(TrainMeDbContext context)
        {
            throw new System.NotImplementedException();
        }

        private void SeedCategories(TrainMeDbContext context)
        {
            throw new System.NotImplementedException();
        }

        private void SeedCourses(TrainMeDbContext context)
        {
            // TODO: fix the if when categories are seeded with images
            if (context.Courses.Any() || !context.Categories.Any())
            {
                return;
            }

            var categories = context.Categories.ToArray();

            var trainerRoleId = context.Roles.First(r => r.Name == RoleNamesConstants.TrainerRoleName).Id;
            var authors = context.Users
                .Where(u => u.Roles.Any(r => r.RoleId == trainerRoleId))
                .ToArray();

            var attendees = context.Users.ToArray();
            var attendeesMaxCount = attendees.Length - 1;

            for (int i = 0; i < 100; i++)
            {
                var currentAuthor = authors[RandomGenerator.GetRandomNumber(0, authors.Length - 1)];

                var currentAttendees = attendees
                    .Where(a => a != currentAuthor)
                    .OrderBy(a => Guid.NewGuid())
                    .Take(RandomGenerator.GetRandomNumber(0, attendeesMaxCount)).ToArray();

                var course = new Course
                {
                    Name = RandomGenerator.GetRandomString(3, ValidationConstants.CourseNameMaxLength),
                    Description = RandomGenerator.GetRandomString(10, ValidationConstants.CourseDescriptionMaxLength),
                    Category = categories[RandomGenerator.GetRandomNumber(0, categories.Length - 1)],
                    Author = currentAuthor,
                    Attendees = currentAttendees
                };

                context.Courses.Add(course);
            }

            context.SaveChanges();
        }
    }
}
