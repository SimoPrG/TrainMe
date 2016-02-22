namespace TrainMe.Web.Areas.Admin.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Common;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CategoryInputModel : IMapTo<Category>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        public string ImageFileName { get; set; }
    }
}
