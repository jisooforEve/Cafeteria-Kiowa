using System.Diagnostics;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using ProyectoDSI_Avance.Models;

namespace ProyectoDSI_Avance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ComidaSeccion()
        {
            return View();
        }

        public IActionResult ComplementosChilaquiles()
        {
            return View();
        }

        public IActionResult ComplementosEnchiladas() {
        
          return View();
        }

        public IActionResult ComplementosPalomitasPollo()
        {
            return View();
        }

        public IActionResult ComplementosHotDog()
        {
            return View();
        }

        public IActionResult ComplementosTengers()
        {
            return View();
        }

        public IActionResult ComplementoPirata()
        {
            return View();
        }

        public IActionResult ComplementoGringa()
        {
            return View();
        }

        public IActionResult PostresSeccion()
        {
            return View();
        }

        public IActionResult BebidasSeccion()
        {
            return View();
        }

        public IActionResult ComplementosLicuado() { 
        
            return View();
        }

        public IActionResult CuentaPago()
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
