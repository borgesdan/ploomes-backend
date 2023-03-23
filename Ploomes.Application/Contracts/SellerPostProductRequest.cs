namespace Ploomes.Application.Contracts
{
    public class SellerPostProductRequest
    {
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? SellerUid { get; set; }
    }
}