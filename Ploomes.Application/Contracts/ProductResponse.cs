using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Contracts
{
    public class ProductResponse
    {
        public string? Uid { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }        
        public string? Description { get; set; }

        public ProductResponse(ProductEntity entity)
        {
            Title = entity.Title;
            Price = entity.Price;            
            Description = entity.Description;
            Uid = entity.Uid.ToString().ToLower();
        }
    }
}
