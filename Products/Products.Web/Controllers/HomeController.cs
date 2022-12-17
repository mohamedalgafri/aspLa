using Microsoft.AspNetCore.Mvc;
using Products.Web.Data;
using System.Diagnostics;

namespace Products.Web.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _db;

        public HomeController( ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            var bb = _db.Products.ToList();
            return View(bb);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}