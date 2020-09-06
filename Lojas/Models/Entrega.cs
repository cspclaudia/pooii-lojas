using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    [Table("Entrega")]
    public class Entrega
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        
        [Required] 
        public string Destino { get; set; }
        
        [Required] 
        [Column (TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
        
        [Required] 
         public int PedidoId { get; set; }


        [ForeignKey("PedidoId")] 
        public Pedido Pedido { get; set; }
    }
}