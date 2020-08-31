using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; }

        [DataType (DataType.Date)]
        public DateTime Data { get; set; }

        [Column (TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
        public Loja Loja { get; set; }
    }
}