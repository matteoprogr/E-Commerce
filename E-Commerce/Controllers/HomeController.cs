using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

      
        public IActionResult Index(Carrello carrello)
        {
            var sessioneAperta = HttpContext.Session.Keys.Contains("carrello");
            if (!sessioneAperta)
            {
                try
                {
                    Carrello carrelloSessione = new Carrello();
                    HttpContext.Session.SetString("carrello", JsonConvert.SerializeObject(carrello));
                    ViewData["carrello"] = "sessione aperta";
                    

                }
                catch
                {
                    return View();
                }

            }
            string? user = HttpContext.Session.GetString("username");
            if (user != null)
            {
                Clienti? clienti = JsonConvert.DeserializeObject<Clienti?>(user!);
                ViewData["user"] = clienti;
            }
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}
