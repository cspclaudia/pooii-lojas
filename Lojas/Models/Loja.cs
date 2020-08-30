using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    public class Loja
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public Estoque Estoque { get; set; }

        // [Display (Name = "Release Date")]
        // [DataType (DataType.Date)]
        // public DateTime ReleaseDate { get; set; }
    }
}