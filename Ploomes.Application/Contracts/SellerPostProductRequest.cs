namespace Ploomes.Application.Contracts
{
    public class SellerPostProductRequest
    {
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public string? Description { get; set; }
        public string? SellerUid { get; set; }
        public int Count { get; set; }
    }
}