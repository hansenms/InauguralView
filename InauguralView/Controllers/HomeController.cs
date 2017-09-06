using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InauguralView.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Documents;

namespace InauguralView.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";

            SpielEntry s = await DocumentDBRepository<SpielEntry>.GetItemAsync("5bcfaaf7-508c-44c5-bf45-6fa5df30c27b");
 
            Console.WriteLine("Spiel speaker: " + s.Spiel.Speaker);

            ViewData["Speaker"] = s.Spiel.Speaker;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
