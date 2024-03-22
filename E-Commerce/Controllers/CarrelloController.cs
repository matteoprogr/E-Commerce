using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce.Controllers
{
    public class CarrelloController : Controller
    {
        private readonly IRepositoryProdotti? repositoryProdotti;

        public CarrelloController(IRepositoryProdotti? repositoryProdotti)
        {
            this.repositoryProdotti = repositoryProdotti;
        }

        public IActionResult Index()
        {
            var Car = HttpContext.Session.GetString("carrello");


            var carrello = JsonConvert.DeserializeObject<Carrello>(Car);
            
            ViewData["addCarello"] = carrello;


            return View();
        }

        public IActionResult RiepilogoOrdine()
        {
            var riepilogo = HttpContext.Session.GetString("riepilogo");
            var riepilogoDes = JsonConvert.DeserializeObject<Riepilogo?>(riepilogo);

            return View(riepilogoDes);
        }

        [HttpPost]
        public IActionResult Rimuovi(string[] rim)
        {
            var carrello = HttpContext.Session.GetString("carrello");
            var carrelloDes = JsonConvert.DeserializeObject<Carrello?>(carrello);
            int i = 0;
            Carrello newCarrello= new Carrello();

            if (rim.Length !=0)
            {
                foreach (var v in carrelloDes?.prodottoSelezionato)
                {
                    if (!rim[i].Equals(v.Nome))
                    {
                        newCarrello.AddProdotto(v);
                    }
                    else
                    {
                        RitornaProdotti(v.Nome,v.QuantitaSelezionata);
                    }
                    i++;
                }
                HttpContext.Session.Remove("carrello");
                HttpContext.Session.SetString("carrello", JsonConvert.SerializeObject(newCarrello));
            }
            else
            {
                TempData["niente"] = "Selezionare un oggetto da rimuovere";
            }
            

            return RedirectToAction("Index","Carrello");

        }

        [HttpPut]
        public IActionResult RitornaProdotti(string nome, int Quantita)
        {
            repositoryProdotti?.AggiungiQuantita(nome,Quantita);

            return Ok();
        }
    }
}
