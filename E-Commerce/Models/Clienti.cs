using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [Table("Clienti")]
    [Index("Email",IsUnique = true)]
    public class Clienti
    {
        [Key]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Email { get; set; }

        public ICollection<Ordini> OrdiniCliente { get; set; }
    }
}
