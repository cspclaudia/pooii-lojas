using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    [Table("Produto_Pedido")]
    public class Produto_Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required] 
        public int Quantidade { get; set; }

        [Required] 
        public int ProdutoId { get; set; }

        // [Required] 
        public int PedidoId { get; set; }


        [ForeignKey("ProdutoId")] 
        public Produto Produto { get; set; }
        
        [ForeignKey("PedidoId")] 
        public Pedido Pedido { get; set; }
    }
}