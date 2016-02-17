namespace TrainMe.Services.Data
{
    using System;
    using System.IO;
    using System.Web;
    using TrainMe.Data.Contracts;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Base;
    using TrainMe.Services.Data.Contracts;

    public class FileService : BaseService, IFileService
    {
        public FileService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public FileDetail GetById(string id)
        {
            return this.UnitOfWork.FileDetails.GetById(Guid.Parse(id));
        }

        public FileDetail Add(HttpPostedFileBase upload, string saveDirectory)
        {
            if (upload == null || upload.ContentLength == 0)
            {
                throw new ArgumentNullException(nameof(upload));
            }

            var guid = Guid.NewGuid();
            var newFileName = guid + Path.GetExtension(upload.FileName);
            var file = new FileDetail
            {
                Id = guid,
                ContentType = upload.ContentType,
                OriginalFileName = Path.GetFileName(upload.FileName),
                FileName = newFileName,
                FilePath = Path.Combine(saveDirectory, newFileName)
            };

            upload.SaveAs(file.FilePath);

            this.UnitOfWork.FileDetails.Add(file);
            this.UnitOfWork.Commit();

            return file;
        }

        public void DeleteById(Guid fileId)
        {
            this.UnitOfWork.FileDetails.Delete(fileId);
            this.UnitOfWork.Commit();
        }
    }
}
