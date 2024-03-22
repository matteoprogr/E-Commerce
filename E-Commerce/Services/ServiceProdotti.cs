using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

namespace E_Commerce.Services
{
    public class ServiceProdotti:IRepositoryProdotti
    {
        private readonly DbContesto _dbcontesto;

        public ServiceProdotti(DbContesto dbcontesto)
        {
            _dbcontesto = dbcontesto;
        }

        public void AggiungiQuantita(string nome, int QuantitaSelezionata)
        {
            List<Prodotti> lista = (List<Prodotti>) findProdotti();
            foreach (var v in lista)
            {
                if (nome.Equals(v.Nome))
                {
                    v.Quantita += QuantitaSelezionata;
                    _dbcontesto.Update(v);
                    _dbcontesto.SaveChanges();
                }
            }
        }

        public IEnumerable<Prodotti> findProdotti()
        {
            List<Prodotti> lista =  _dbcontesto.Prodotti.ToList();

            return lista;

        }

        public async Task<IEnumerable<Prodotti>> loadProdottiById(int? idCategoria)
        {
            List<Prodotti> lista= await _dbcontesto.Prodotti!.ToListAsync();
            List<Prodotti> listaFiltrata = new List<Prodotti>();

            foreach (var item in lista)
            {
                if (item.IdCategoria == idCategoria)
                {
                    listaFiltrata.Add(item);
                }
            }
            return  listaFiltrata;

        }

        public void ModificaQuantita(string? nome,int QuantitaSelezionata)
        {
            IEnumerable<Prodotti> lista =  findProdotti();

            // Ora puoi eseguire la modifica della quantità sui prodotti nella lista
            foreach (var prodotto in lista)
            {
                // Esempio di modifica della quantità (assicurati di adattare questa parte alla tua logica specifica)
                if (prodotto!.Nome.Equals(nome))
                {
                    prodotto.Quantita -= QuantitaSelezionata;
                    _dbcontesto.Update(prodotto);
                    _dbcontesto.SaveChanges();
                }
            }   


            // Salva le modifiche nel database
            

        }
    }
}
