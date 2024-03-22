using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_Commerce.Controllers
{
    public class SessioneController : Controller
    {
        private readonly DbContesto dbContesto;

        public SessioneController(DbContesto dbContesto)
        {
            this.dbContesto = dbContesto;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Clienti cliente)
        {
            ViewData["err"] = "Inserisci credenziali";
            // Verifica delle credenziali dell'utente nel database
            var userN = await dbContesto.Clienti
                .FirstOrDefaultAsync(u => u.Username == cliente.Username);
            var userP = await dbContesto.Clienti
                .FirstOrDefaultAsync(u => u.Password == cliente.Password);

            if (userN != null && userP != null)
            {
                HttpContext.Session.SetString("username", JsonConvert.SerializeObject(userN));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (userN == null && userP == null)
                {
                    TempData["err"] = "Username e password errati";
                }
                else if(userN == null)
                {
                    TempData["err"] = "Username errato";
                }
                else if (userP == null)
                {
                    TempData["err"] = "Password errata";
                }

            }

            return RedirectToAction("Login", "Cliente");
        }

        
        public  IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
   
}

