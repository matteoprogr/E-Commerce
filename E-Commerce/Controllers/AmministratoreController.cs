using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Newtonsoft.Json;

namespace E_Commerce.Controllers
{
    public class AmministratoreController : Controller
    {

        private readonly IRepositoryCliente? repositoryCliente;
        private readonly IRepositoryProdotti? repositoryProdotti;

        public AmministratoreController(IRepositoryCliente? repositoryCliente, IRepositoryProdotti? repositoryProdotti)
        {
            this.repositoryCliente = repositoryCliente;
            this.repositoryProdotti = repositoryProdotti;
        }

        public IActionResult Dettagli_Clienti()
        {
            
            List<Clienti> lista = repositoryCliente!.loadClienti();
            ViewData["listaClienti"] = lista;
            

            return View();
        }

        public IActionResult Dettagli_Prodotti()
        {
           
            List<Prodotti> lista = (List<Prodotti>)repositoryProdotti.findProdotti();
            ViewData["listaProdotti"] = lista;


            return View();
        }
    }
}
