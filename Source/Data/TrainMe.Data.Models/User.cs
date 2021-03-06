﻿namespace TrainMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TrainMe.Data.Models.Contracts;
    using TrainMe.Web.Infrastructure.Common;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IBaseModel
    {
        private ICollection<Course> createdCourses;
        private ICollection<Course> attendedCourses;

        public User()
        {
            this.createdCourses = new HashSet<Course>();
            this.attendedCourses = new HashSet<Course>();

            if (this.CreatedOn == default(DateTime))
            {
                this.CreatedOn = DateTime.UtcNow;
            }
        }

        [Required]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<Course> CreatedCourses
        {
            get { return this.createdCourses; }
            set { this.createdCourses = value; }
        }

        public virtual ICollection<Course> AttendedCourses
        {
            get { return this.attendedCourses; }
            set { this.attendedCourses = value; }
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
