using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    public class Estoque
    {
        [Key] public int Id { get; set; }
        [Required] public int Quantidade { get; set; }
        [Required] public int LojaId { get; set; }
        [Required] public int ProdutoId { get; set; }
        
        [ForeignKey("LojaId")] public Loja Loja { get; set; }
        [ForeignKey("ProdutoId")] public Produto Produto { get; set; }

    }
}