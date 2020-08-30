using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    public class Entrega
    {
        public int Id { get; set; }
        public string Destino { get; set; }
        
        [Column (TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
        public Pedido Pedido { get; set; }
    }
}