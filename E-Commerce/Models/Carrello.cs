namespace E_Commerce.Models
{
    public class Carrello
    {
        public List<ProdottoSelezionato>? prodottoSelezionato { get; set; }= new List<ProdottoSelezionato> { };

        public void AddProdotto(ProdottoSelezionato? prodottoSelezionato)
        {
            this.prodottoSelezionato!.Add(prodottoSelezionato);
            
        }
    }
}
