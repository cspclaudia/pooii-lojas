using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public Produto Produto { get; set; }
        public Loja Loja { get; set; }

    }
}