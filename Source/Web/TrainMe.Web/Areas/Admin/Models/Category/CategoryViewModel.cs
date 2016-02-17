namespace TrainMe.Web.Areas.Admin.Models.Category
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using TrainMe.Common;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        [UIHint("FileUpload")]
        [DisplayName("Image")]
        [Required]
        public string ImageFileName { get; set; }
    }
}
