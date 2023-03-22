using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Contracts
{
    public class BuyerPostCreateOrderResponse
    {
        public string? OrderUid { get; set; }

        public BuyerPostCreateOrderResponse(OrderEntity entity) 
        {
            OrderUid = entity.Uid.ToString().ToLower();
        }
    }
}
