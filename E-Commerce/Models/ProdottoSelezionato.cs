using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    
    public class ProdottoSelezionato:Prodotti
    {
        
        public int QuantitaSelezionata { get; set; }

    }
}
