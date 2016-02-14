namespace TrainMe.Services.Data.Contracts
{
    using System.Collections.Generic;
    using TrainMe.Data.Models;

    public interface ISomeModelService
    {
        IEnumerable<SomeModel> GetAll();

        IEnumerable<SomeModel> GetTop(int count);
    }
}
