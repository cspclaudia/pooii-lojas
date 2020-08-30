using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
    }
}