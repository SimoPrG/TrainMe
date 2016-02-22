namespace TrainMe.Web.Areas.Admin.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Areas.Admin.ViewModels.Category;
    using TrainMe.Web.Common;
    using TrainMe.Web.Infrastructure.Mapping;

    public class CategoryController : AdminController
    {
        private readonly IFileService fileService;
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService, IFileService fileService)
        {
            this.fileService = fileService;
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result =
                this.categoryService.All()
                .To<CategoryViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, CategoryInputModel categoryInputModel)
        {
            CategoryViewModel categoryViewModel = null;

            if (this.ModelState.IsValid)
            {
                var category = this.Mapper.Map<Category>(categoryInputModel);

                // TODO: find a better way to pass the ImageId
                string fileName = categoryInputModel.ImageFileName;
                category.ImageId = Guid.Parse(fileName.Remove(fileName.IndexOf('.')));

                this.categoryService.Add(category);

                categoryViewModel = this.Mapper.Map<CategoryViewModel>(category);
                categoryViewModel.ImageFileName = categoryInputModel.ImageFileName;
            }

            return this.Json(new[] { categoryViewModel }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, CategoryInputModel categoryInputModel)
        {
            CategoryViewModel categoryViewModel = null;

            if (this.ModelState.IsValid)
            {
                var category = this.categoryService.GetById(categoryInputModel.Id);

                category.Name = categoryInputModel.Name;

                // TODO: find a better way to pass the ImageId
                string fileName = categoryInputModel.ImageFileName;
                category.ImageId = Guid.Parse(fileName.Remove(fileName.IndexOf('.')));

                this.categoryService.Update();

                categoryViewModel = this.Mapper.Map<CategoryViewModel>(category);
            }

            return this.Json(new[] { categoryViewModel }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CategoryInputModel categoryInputModel)
        {
            CategoryViewModel categoryViewModel = null;

            if (this.ModelState.IsValid)
            {
                var category = this.categoryService.DeleteById(categoryInputModel.Id);

                this.fileService.DeleteById(category.ImageId);

                this.categoryService.Update();

                categoryViewModel = this.Mapper.Map<CategoryViewModel>(category);
            }

            return this.Json(new[] { categoryViewModel }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult Save(HttpPostedFileBase categoryImage)
        {
            var image = this.fileService.Add(categoryImage, Constants.CategoriesImageDirectory);
            return this.Json(new { ImageFileName = image.FileName });
        }
    }
}
