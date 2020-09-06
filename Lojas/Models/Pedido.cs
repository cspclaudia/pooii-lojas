using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public string Cliente { get; set; }

        [Required]
        [DataType (DataType.DateTime)]
        public DateTime Data { get; set; }

        [Required]
        [Column (TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }

        [Required]
        public int LojaId { get; set; }


        [ForeignKey("LojaId")] 
        public Loja Loja { get; set; }
    }
}