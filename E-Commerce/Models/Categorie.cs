using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [Table("Categorie")]
    [Index("Nome", IsUnique = true)]
    public class Categorie
    {

        [Key]
        public int IdCategoria { get; set; }

        [Required]
        public string? Nome { get; set; }

        public ICollection<Prodotti> Prodotti { get; set; }

    }
}
