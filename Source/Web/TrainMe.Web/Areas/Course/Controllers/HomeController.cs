namespace TrainMe.Web.Areas.Course.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using TrainMe.Data;
    using TrainMe.Data.Models;
    using TrainMe.Services.Data.Contracts;
    using TrainMe.Web.Areas.Course.ViewModels;
    using TrainMe.Web.Areas.Course.ViewModels.Category;
    using TrainMe.Web.Areas.Course.ViewModels.Course;
    using TrainMe.Web.Controllers;
    using TrainMe.Web.Infrastructure.Common;
    using TrainMe.Web.Infrastructure.Mapping;

    public class HomeController : BaseController
    {
        private readonly ICourseService courseService;
        private readonly ICategoryService categoryService;

        public HomeController(ICourseService courseService, ICategoryService categoryService)
        {
            this.courseService = courseService;
            this.categoryService = categoryService;
        }

        [ValidateInput(false)]
        public ActionResult Index(string querry, string category, string orderBy, int page = 1)
        {
            const int PageSize = 10;

            int totalCoursesCount = this.courseService.CountCourses(querry, category);
            int totalPages = (totalCoursesCount + PageSize - 1) / PageSize;

            page = Validator.ValidatePage(page, totalPages);

            var courseViewModels =
                this.courseService.All(querry, category, orderBy, page, PageSize).To<CourseViewModel>().ToList();

            var allCategories = this.Cache.Get<IEnumerable<SelectListItem>>(
                "allCategories",
                () => this.categoryService.All().To<CategoryViewModel>().To<SelectListItem>().ToList(),
                2 * 60 * 60);

            var indexViewModel = new IndexViewModel
            {
                CourseViewModels = courseViewModels,
                AllCategories = allCategories,
                Category = category,
                Querry = querry,
                OrderBy = orderBy,
                OrderByNameParam = string.IsNullOrEmpty(orderBy) ? QuerryStrings.CourseNameDesc : string.Empty,
                OrderByCategoryParam = orderBy == QuerryStrings.CourseCategoryName ?
                    QuerryStrings.CourseCategoryNameDesc : QuerryStrings.CourseCategoryName,
                OrderByAuthorParam = orderBy == QuerryStrings.CourseAuthorName ?
                    QuerryStrings.CourseAuthorNameDesc : QuerryStrings.CourseAuthorName,
                Page = page,
                TotalPages = totalPages
            };

            return this.View(indexViewModel);
        }
    }
}
