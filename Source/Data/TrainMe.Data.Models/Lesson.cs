namespace TrainMe.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Common;
    using TrainMe.Data.Models.Base;
    using TrainMe.Data.Models.Contracts;

    public class Lesson : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.LessonTitleMaxLength)]
        public string Title { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
