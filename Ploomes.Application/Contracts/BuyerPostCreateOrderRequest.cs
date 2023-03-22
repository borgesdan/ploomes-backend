namespace Ploomes.Application.Contracts
{
    public class BuyerPostCreateOrderRequest
    {
        public string? BuyerUid { get; set; }
        public string? ProductUid { get; set; }
        public int ProductCount { get; set; }
    }
}