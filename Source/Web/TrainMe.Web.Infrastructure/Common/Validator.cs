namespace TrainMe.Web.Infrastructure.Common
{
    public static class Validator
    {
        public static int ValidatePage(int page, int totalPages)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (totalPages == 0)
            {
                page = 1;
            }
            else if (totalPages < page)
            {
                page = totalPages;
            }

            return page;
        }
    }
}
