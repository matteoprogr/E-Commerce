using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IRepositoryCategorie
    {
        public Task< IEnumerable<Categorie>> loadCategorie();
    }
}
