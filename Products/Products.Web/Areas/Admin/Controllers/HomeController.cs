using Microsoft.AspNetCore.Mvc;
using Products.Web.Areas.Admin.Controllers;
using Products.Web.Admin.Models;
using System.Diagnostics;

namespace Products.Web.Admin.Controllers
{
    public class HomeController : BaseController
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
    }
}