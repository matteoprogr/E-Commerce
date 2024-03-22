using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [Table("Ordini")]
    public class Ordini
    {
        [Key]
        public int IdOrdini { get; set; }

        [Required]
        public DateTime DataOrdine { get; set; }

        [ForeignKey("Cliente")]
        public string? UsernameCliente { get; set; }

        [Required]
        public Clienti? Cliente { get; set; }

        [Required]
        public string? TipoPagamento {  get; set; }

        [Required]
        public string? TipoSpedizione {  get; set; }

        [Required]
        public string? DettagliOrdini { get; set; }

        [NotMapped]
        public List<string> tipiPagamento= new List<string>
        {
            "Seleziona",
            "paypal",
            "Carta di credito",
            "Bitcoin"
        };


        [NotMapped]
        public List<string> tipiSpedizione = new List<string>
        {
            "Seleziona",
            "PonyExpress",
            "A piedi",
            "Standard",
            "A cavallo"
        };

    }
}
