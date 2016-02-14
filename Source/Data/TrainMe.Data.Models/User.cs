namespace TrainMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TrainMe.Common;
    using TrainMe.Data.Models.Contracts;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IBaseModel
    {
        private ICollection<SomeModel> someModels;

        public User()
        {
            this.someModels = new HashSet<SomeModel>();

            if (this.CreatedOn == default(DateTime))
            {
                this.CreatedOn = DateTime.UtcNow;
            }
        }

        [Required]
        [MaxLength(ModelValidationConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.NameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<SomeModel> SomeModels
        {
            get { return this.someModels; }
            set { this.someModels = value; }
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
