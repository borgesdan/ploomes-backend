namespace Ploomes.Application.Contracts
{
    public class SellerPostProductResponse
    {
        public string? ProductUid { get; set; }

        public SellerPostProductResponse(Guid uid) 
        {
            ProductUid = uid.ToString().ToLower();
        }
    }
}