namespace TrainMe.Web.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;

    public class FileController : BaseController
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public ActionResult GetFile(string id)
        {
            var file = this.fileService.GetById(id);
            return this.File(file.FilePath, file.ContentType);
        }
    }
}
