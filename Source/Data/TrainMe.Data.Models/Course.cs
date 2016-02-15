namespace TrainMe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Common;
    using TrainMe.Data.Models.Contracts;

    public class Course : BaseModel
    {
        private ICollection<User> attendees;

        public Course()
        {
            this.attendees = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.CourseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.CourseDescriptionMaxLength)]
        public string Description { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<User> Attendees
        {
            get { return this.attendees; }
            set { this.attendees = value; }
        }
    }
}
