using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proiect_UrsuAlexandra.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect_UrsuAlexandra.Data;
using Proiect_UrsuAlexandra.Models.LibraryViewModels;


namespace Proiect_UrsuAlexandra.Controllers
{
    public class HomeController : Controller
    {
        private readonly AgencyContext _context;

        public HomeController(AgencyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
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

        public async Task<ActionResult> Statistics()
        {
            IQueryable<ApplicationGroup> data =
            from application in _context.Applications
            group application by application.ApplicationDate into dateGroup
            select new ApplicationGroup()
            {
                ApplicationDate = dateGroup.Key,
                JobCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Chat()
        {
            return View();
        }

    }
}
