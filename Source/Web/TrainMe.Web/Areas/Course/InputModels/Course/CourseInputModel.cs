namespace TrainMe.Web.Areas.Course.InputModels.Course
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using TrainMe.Data.Models;
    using TrainMe.Web.Infrastructure.Common;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CourseInputModel : IMapTo<Course>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CourseNameMaxLength)]
        [AllowHtml]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CourseDescriptionMaxLength)]
        [AllowHtml]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> AllCategories { get; set; }
    }
}
