using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text;

namespace E_Commerce.Controllers
{
    public class OrdiniController : Controller
    {
        private readonly IRepositoryOrdini? repositoryOrdini;
        private readonly IRepositoryCategorie? repositoryCategorie;
        private readonly IRepositoryProdotti? repositoryProdotti;
        

        public OrdiniController(IRepositoryOrdini? repositoryOrdini, IRepositoryCategorie? repositoryCategorie, IRepositoryProdotti? repositoryProdotti)
        {
            this.repositoryOrdini = repositoryOrdini;
            this.repositoryCategorie = repositoryCategorie;
            this.repositoryProdotti = repositoryProdotti;
        }

        public async Task<IActionResult> Ordine()
        {
            SelectList select;
            select = new SelectList(await repositoryCategorie!.loadCategorie(),"IdCategoria","Nome","Prodotti");
            ViewData["Select"] = select;

            return View();
        }

        public async Task<ActionResult<IEnumerable<Prodotti>>> AllProducts()
        {
            List<Prodotti> lista = (List<Prodotti>) repositoryProdotti!.findProdotti();
            return lista;
        }

        [HttpPost]
       public async Task<ActionResult<IEnumerable<Prodotti>>> Prodotti([FromBody] int id)
        {
            List<Prodotti> lista = new List<Prodotti>();
            if (id != 0)
            {
                lista = (List<Prodotti>)await repositoryProdotti!.loadProdottiById(id);
            }
            else
            {
                lista = (List<Prodotti>) repositoryProdotti!.findProdotti();
            }
            
           
            return lista;
        }



        
        [HttpPost]
        public  IActionResult Carrello([FromBody] AddCarrello ps)
        {
            StringBuilder stb=new StringBuilder();
            ProdottoSelezionato sp = new ProdottoSelezionato();
            sp.Nome = ps.Nome;
            string prezzo=ps.Prezzo.Replace(".",",");
            sp.Prezzo = double.Parse(prezzo);
            sp.QuantitaSelezionata =int.Parse(ps.QuantitaSelezionata);

            var Car = HttpContext.Session.GetString("carrello");
            var carrello = JsonConvert.DeserializeObject<Carrello>(Car);
            bool presente = false;

            if (sp.QuantitaSelezionata > 0)
            {
                if (carrello.prodottoSelezionato.Count != 0)
                {
                    foreach (var v in carrello.prodottoSelezionato)
                    {
                        if (v.Nome.Equals(sp.Nome))
                        {
                            v.QuantitaSelezionata += sp.QuantitaSelezionata;
                            presente = true;
                            break;
                        }

                    }
                    modifica(sp.Nome, sp.QuantitaSelezionata);
                }
                if (carrello.prodottoSelezionato.Count == 0 || presente == false)
                {
                    carrello.AddProdotto(sp);
                    modifica(sp.Nome, sp.QuantitaSelezionata);
                }
                presente = false;
            }
            else
            {
                TempData["zero"] = "Selezionare almeno un prodotto";
            }

            HttpContext.Session.SetString("carrello", JsonConvert.SerializeObject(carrello));



            return Ok();
        }
        [HttpPut]
        public ActionResult modifica(string nome,int quantita)
        {
            repositoryProdotti!.ModificaQuantita(nome, quantita);

            return Ok();
        }

        [HttpPost]
        public async Task <ActionResult> ConfermaOrdine(string pagamento,string spedizione,int[] mQ) 
        {
            var Car = HttpContext.Session.GetString("carrello");
            var carrello = JsonConvert.DeserializeObject<Carrello>(Car);
            var userSer = HttpContext.Session.GetString("username");
            if (userSer != null)
            {
                Riepilogo rp = new Riepilogo();
                var userDes = JsonConvert.DeserializeObject<Clienti>(userSer);
                Ordini ord = new Ordini();
                DateTime dt = DateTime.Now;
                ord.DataOrdine = dt;
                ord.UsernameCliente = userDes.Username;
                string? dettagliordine = "";
                double? totale = 0;
                int i = 0;
                foreach (var v in carrello!.prodottoSelezionato)
                {
                    dettagliordine += "Prodotto: " + v.Nome + ", Quantità selezionata: " + mQ[i] + ", Prezzo: " + v.Prezzo+$" TotaleProdotto: {v.Prezzo * mQ[i]}";
                    totale += v.Prezzo * mQ[i];
                    rp.riepilogoList.Add(new Riepilogo(v.Nome, mQ[i]+"",v.Prezzo+"", (v.Prezzo * mQ[i])+"",totale));
                    i++;
                }
                ord.DettagliOrdini = dettagliordine +" Totale Ordine:"+ totale+"";
                ord.TipoPagamento = pagamento;
                ord.TipoSpedizione = spedizione;

                HttpContext.Session.SetString("riepilogo", JsonConvert.SerializeObject(rp));


                await repositoryOrdini?.SaveOrdine(ord);

                HttpContext.Session.Remove("carrello");
                return RedirectToAction("RiepilogoOrdine", "Carrello");
            }
            else
            {
                TempData["accesso"] = "Effettuare l'accesso";
                return RedirectToAction("Login", "Cliente");
            }
            

        }
    }
}
