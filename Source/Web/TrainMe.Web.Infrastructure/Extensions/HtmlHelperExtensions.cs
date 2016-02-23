namespace TrainMe.Web.Infrastructure.Extensions
{
    using System.Web.Mvc;

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string url, string alternateText, string height)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alternateText);
            builder.MergeAttribute("height", height);
            string imgHtml = builder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(imgHtml);
        }
    }
}
