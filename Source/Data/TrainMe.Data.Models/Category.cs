namespace TrainMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Common;
    using TrainMe.Data.Models.Base;

    public class Category : BaseModel
    {
        private ICollection<Course> courses;

        public Category()
        {
            this.courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        public Guid ImageId { get; set; }

        public virtual FileDetail Image { get; set; }

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }
    }
}
