namespace TrainMe.Web.Areas.Admin.InputModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Common;
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
