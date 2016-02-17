namespace TrainMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Common;
    using TrainMe.Data.Models.Base;

    public class Category : BaseModel
    {
        private ICollection<SubCategory> subCategories;

        public Category()
        {
            this.subCategories = new HashSet<SubCategory>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        public Guid ImageId { get; set; }

        public virtual FileDetail Image { get; set; }

        public virtual ICollection<SubCategory> SubCategories
        {
            get { return this.subCategories; }
            set { this.subCategories = value; }
        }
    }
}
