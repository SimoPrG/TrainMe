﻿namespace TrainMe.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using TrainMe.Common;

    public class RegisterViewModel
    {
        [Required]
        [MaxLength(ModelValidationConstants.UserNameMaxLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.UserNameMaxLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.UserNameMaxLength)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Are you a trainer?")]
        public bool Trainer { get; set; }
    }
}
