using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class DbContesto : DbContext
    {
        public DbContesto(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Clienti> Clienti { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Ordini> Ordini { get; set; }
        public DbSet<Prodotti> Prodotti { get; set; }
    }
}
