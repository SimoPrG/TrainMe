namespace TrainMe.Services.Data.Contracts
{
    using System;
    using System.Web;
    using TrainMe.Data.Models;

    public interface IFileService
    {
        FileDetail GetById(string id);

        FileDetail Add(HttpPostedFileBase upload, string saveDirectory);

        void DeleteById(Guid fileId);
    }
}
