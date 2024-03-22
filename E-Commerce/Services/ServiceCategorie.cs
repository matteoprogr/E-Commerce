using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class ServiceCategorie:IRepositoryCategorie
    {
        private readonly DbContesto _dbcontesto;

        public ServiceCategorie(DbContesto dbcontesto)
        {
            _dbcontesto = dbcontesto;
        }

        public async Task<IEnumerable<Categorie>> loadCategorie()
        {
            return await _dbcontesto.Categorie.ToListAsync();
        }
    }
}
