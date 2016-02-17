namespace TrainMe.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TrainMe.Common;
    using TrainMe.Data.Models.Base;

    public class FileDetail : BaseModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.FileNameMaxLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.FileNameMaxLength)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ContentTypeMaxLength)]
        public string ContentType { get; set; }

        [Required]
        [MaxLength(ValidationConstants.FileNameMaxLength)]
        public string FilePath { get; set; }
    }
}
