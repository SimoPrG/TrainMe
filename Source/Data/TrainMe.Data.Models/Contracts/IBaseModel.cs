namespace TrainMe.Data.Models.Contracts
{
    using System.ComponentModel.DataAnnotations;

    public interface IBaseModel : IAuditInfo, IDeletableEntity
    {
    }
}
