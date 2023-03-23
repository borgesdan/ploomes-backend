namespace Ploomes.Application.Contracts
{
    public class ProductGetAllFilterRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public void Sanitize()
        {
            Page = ((Page <= 0) ? 1 : Page);
            PageSize = ((PageSize <= 0) ? 10 : PageSize);
        }
    }
}
