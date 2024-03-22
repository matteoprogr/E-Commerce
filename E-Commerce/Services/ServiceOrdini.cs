using E_Commerce.Models;
using E_Commerce.Repository;

namespace E_Commerce.Services
{
    public class ServiceOrdini:IRepositoryOrdini
    {
        private readonly DbContesto _dbcontesto;

        public ServiceOrdini(DbContesto dbcontesto)
        {
            _dbcontesto = dbcontesto;
        }

        public async Task SaveOrdine(Ordini? ord)
        {
             _dbcontesto.Ordini?.AddAsync(ord);
            await _dbcontesto.SaveChangesAsync();
        }
    }
}
