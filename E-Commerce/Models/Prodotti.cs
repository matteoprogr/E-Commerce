using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [Table("Prodotti")]
    [Index("Nome", IsUnique = true)]
    public class Prodotti
    {
        [Key]
        public int IdProdotto { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public int Quantita { get; set; }
        [Required]
        public double Prezzo { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        [Required]
        public Categorie? Categoria { get; set; }

        public static implicit operator List<object>(Prodotti? v)
        {
            throw new NotImplementedException();
        }
    }
}
