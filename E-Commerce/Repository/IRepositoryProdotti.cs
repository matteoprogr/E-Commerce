using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IRepositoryProdotti
    {
        public  Task<IEnumerable<Prodotti>> loadProdottiById(int? idCategoria);

        public IEnumerable<Prodotti> findProdotti();

        public void ModificaQuantita(string nome,int QuantitaSelezionata);
        public void AggiungiQuantita(string nome, int QuantitaSelezionata);

    }
}
