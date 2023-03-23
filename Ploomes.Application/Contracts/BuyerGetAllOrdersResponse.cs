using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Contracts
{
    public class BuyerGetAllOrdersResponse
    {
        private static int number = 0;

        public int Number { get; set; }
        public string? OrderUid { get; set; }
        public string? ProductUid { get; set; }
        public string? ProductTitle { get; set; }
        public int ProductCount { get; set; }

        public BuyerGetAllOrdersResponse(OrderEntity entity) 
        {
            Number = ++number;
            OrderUid = entity.Uid.ToString().ToLower();
            ProductUid = entity.ProductUid.ToString().ToLower();
            ProductCount = entity.ProductCount;
            ProductTitle = entity.ProductTitle;
        }
    }
}
