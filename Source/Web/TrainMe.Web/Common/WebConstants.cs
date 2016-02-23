namespace TrainMe.Web.Common
{
    using System;
    using System.IO;

    public static class WebConstants
    {
        public const string HttpRequestItemsCourseKey = "course";

        public const string CategoryImagesPath = "~/Content/images/categories/";
        public static readonly string CategoriesImageDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Content",
            "images",
            "categories");
    }
}
