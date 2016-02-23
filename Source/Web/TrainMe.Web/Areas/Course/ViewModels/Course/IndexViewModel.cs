namespace TrainMe.Web.Areas.Course.ViewModels.Course
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class IndexViewModel
    {
        public IEnumerable<CourseViewModel> CourseViewModels { get; set; }

        public IEnumerable<SelectListItem> AllCategories { get; set; }

        public string Querry { get; set; }

        public string Category { get; set; }

        public string OrderBy { get; set; }

        public string OrderByNameParam { get; set; }

        public string OrderByCategoryParam { get; set; }

        public string OrderByAuthorParam { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
