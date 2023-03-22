using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ploomes.Application.Data.Entities.Sql
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Uid { get; set; }

        [ForeignKey(nameof(Buyer))]
        public int BuyerId { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Range(1, 999)]
        public int ProductCount { get; set; }

        public virtual UserEntity? Buyer { get; set; }
        public virtual ProductEntity? Product { get; set; }

        public OrderEntity() 
        {
            Uid = Guid.NewGuid();
        }
    }
}