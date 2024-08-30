using CMSProductSystem.Models;
using CMSProductSystem.ModelsDB;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMSProductSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        CmsproductSystemContext _db = new CmsproductSystemContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<ProizvodiKategorije> model = (from p in _db.Proizvod
                                               join k in _db.Kategorija
                                               on p.CategoryID equals k.ID
                                               orderby Guid.NewGuid()
                                               select new ProizvodiKategorije { ProizvodPodaci = p, NazivKategorije = k.Naziv }).Take(10).ToList();
            return View(model);
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