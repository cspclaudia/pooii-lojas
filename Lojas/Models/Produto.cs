using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required] 
        public string Nome { get; set; }
        
        [Required] 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
    }
}