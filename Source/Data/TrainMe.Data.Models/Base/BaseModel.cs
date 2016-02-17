namespace TrainMe.Data.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using TrainMe.Data.Models.Contracts;

    public abstract class BaseModel : IBaseModel
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
