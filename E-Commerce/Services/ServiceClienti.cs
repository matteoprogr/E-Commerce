using E_Commerce.Models;
using E_Commerce.Repository;

namespace E_Commerce.Services
{
    public class ServiceClienti:IRepositoryCliente
    {
        private readonly DbContesto _dbcontesto;

        public ServiceClienti(DbContesto dbcontesto)
        {
            _dbcontesto = dbcontesto;
        }

        public List<Clienti> loadClienti()
        {
            List<Clienti> lista= _dbcontesto.Clienti.ToList();

            return lista;
        }

        public List<Ordini> loadOrdiniCliente(string? username)
        {
           List<Ordini> lista = _dbcontesto.Ordini.ToList();
           return lista.Where(or=>or.UsernameCliente.Equals(username)!).ToList();
        }

        public async Task SaveCliente(Clienti? cl)
        {
            _dbcontesto.Clienti.Add(cl);
           await _dbcontesto.SaveChangesAsync();
        }
    }
}
