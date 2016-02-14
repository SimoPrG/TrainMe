namespace TrainMe.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TrainMe.Data.Models.Contracts;

    public class SomeModel : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string SomeProperty { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
