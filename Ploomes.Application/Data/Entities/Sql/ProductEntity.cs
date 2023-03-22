using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ploomes.Application.Data.Shared;

namespace Ploomes.Application.Data.Entities.Sql
{
    /// <summary>Representa um produto de venda (qualquer tipo de anúncio) de um usuário cadastrado como vendedor.</summary>
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Uid { get; set; }

        [Required]
        [StringLength(120)]
        public string? Title { get; set; }

        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Range(0.0, 1.0)]
        public double Discount { get; set; }

        [Required]
        [StringLength(5000)]
        public string? Description { get; set; }

        [ForeignKey(nameof(User))]
        public int SellerId { get; set; }

        public EntityStatus Status { get; set; }

        public virtual UserEntity? User { get; set; }

        public ProductEntity()
        {
            Uid = Guid.NewGuid();
        }
    }
}
