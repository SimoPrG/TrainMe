﻿namespace TrainMe.Web.Areas.Admin.ViewModels.Category
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Common;
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
