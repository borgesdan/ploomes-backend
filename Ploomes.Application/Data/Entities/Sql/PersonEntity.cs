using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ploomes.Application.Data.Entities.Sql
{
    /// <summary>Representa as informações pessoais de um usuário cadastrado.</summary>
    public class PersonEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        [StringLength(50)]
        public string? Document { get; set; }

        public virtual UserEntity? User { get; set; }        
    }
}
