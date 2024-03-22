using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositoryCliente? repositoryCliente;

        public ClienteController(IRepositoryCliente? repositoryCliente)
        {
            this.repositoryCliente = repositoryCliente;
        }

        public IActionResult Cliente()
        {
            var userSer = HttpContext.Session.GetString("username");
            if (userSer != null)
            {
                var userDes = JsonConvert.DeserializeObject<Clienti>(userSer);
                ViewData["user"] = userDes;
            }
            return View();
        }

        public IActionResult Dettagli_Cliente(int id)
        {
            var user = HttpContext.Session.GetString("username");
            var userDes = JsonConvert.DeserializeObject<Clienti?>(user);
            userDes.OrdiniCliente = repositoryCliente!.loadOrdiniCliente(userDes.Username);

            

            return View(userDes);
        }

        [HttpPost]
        public async Task<IActionResult> Save (Clienti cl)
        {
            await repositoryCliente!.SaveCliente(cl);
            return RedirectToAction("Cliente");
        }
        public IActionResult Login()
        {
            return View();
        }

        
        
    }
}
