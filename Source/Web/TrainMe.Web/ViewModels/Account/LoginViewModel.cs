namespace TrainMe.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using TrainMe.Common;

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        [MaxLength(ModelValidationConstants.UserNameMaxLength)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
