using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IRepositoryCliente
    {
        public Task SaveCliente(Clienti cl);

        public List<Ordini> loadOrdiniCliente(string username);

        public List<Clienti> loadClienti();
    }
}
