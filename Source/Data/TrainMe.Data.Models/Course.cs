namespace TrainMe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Common;
    using TrainMe.Data.Models.Base;
    using TrainMe.Data.Models.Contracts;

    public class Course : BaseModel
    {
        private ICollection<User> attendees;
        private ICollection<Lesson> lessons;

        public Course()
        {
            this.attendees = new HashSet<User>();
            this.lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CourseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CourseDescriptionMaxLength)]
        public string Description { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<User> Attendees
        {
            get { return this.attendees; }
            set { this.attendees = value; }
        }

        public virtual ICollection<Lesson> Lessons
        {
            get { return this.lessons; }
            set { this.lessons = value; }
        }
    }
}
