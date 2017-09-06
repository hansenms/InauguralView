using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InauguralView.Models;
using Microsoft.Azure.Documents;
using Microsoft.AspNetCore.Http;

namespace InauguralView.Controllers
{
    public class SpielController : Controller
    {
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            SpielEntry s = await DocumentDBRepository<SpielEntry>.GetItemAsync(id);
            return View(s);
        }

        public async Task<IActionResult> Index()
        {
            var spiels = await DocumentDBRepository<SpielEntry>.GetItemsAsync(d => true);
            return View(spiels);
        }
    }
}