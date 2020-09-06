using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojas.Models
{
    [Table("Loja")]
    public class Loja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required] 
        public string Nome { get; set; }
        
        [Required] 
        public string Local { get; set; }

        // [Display (Name = "Release Date")]
        // [DataType (DataType.Date)]
        // public DateTime ReleaseDate { get; set; }
    }
}