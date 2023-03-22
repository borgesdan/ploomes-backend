using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Contracts
{
    public class BuyerGetAllOrdersResponse
    {
        public string? OrderUid { get; set; }
        public string? ProductUid { get; set; }
        public string? ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductCount { get; set; }

        public BuyerGetAllOrdersResponse(OrderEntity entity) 
        {
            OrderUid = entity.Uid.ToString().ToLower();
            ProductUid = entity.Product.Uid.ToString().ToLower();
            ProductTitle = entity.Product.Title;
            ProductPrice = entity.Product.Price;
            ProductDescription = entity.Product.Description;
            ProductCount = entity.ProductCount;
        }
    }
}
