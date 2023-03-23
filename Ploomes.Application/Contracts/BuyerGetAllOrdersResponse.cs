using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Contracts
{
    public class BuyerGetAllOrdersResponse
    {
        public int Number { get; set; }
        public string? OrderUid { get; set; }
        public string? ProductUid { get; set; }
        public string? ProductTitle { get; set; }
        public int ProductCount { get; set; }

        public BuyerGetAllOrdersResponse(int orderNumber, OrderEntity entity) 
        {
            Number = orderNumber;
            OrderUid = entity.Uid.ToString().ToLower();
            ProductUid = entity.ProductUid.ToString().ToLower();
            ProductCount = entity.ProductCount;
            ProductTitle = entity.ProductTitle;
        }
    }
}
