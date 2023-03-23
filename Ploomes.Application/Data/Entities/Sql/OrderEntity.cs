using System.ComponentModel.DataAnnotations;

namespace Ploomes.Application.Data.Entities.Sql
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Uid { get; set; }

        [Required]
        public Guid BuyerUid { get; set; }

        [Required]
        public Guid ProductUid { get; set; }

        [Required]
        public string? ProductTitle { get; set; }

        [Range(1, 999)]
        public int ProductCount { get; set; }

        public OrderEntity() 
        {
            Uid = Guid.NewGuid();
        }
    }
}