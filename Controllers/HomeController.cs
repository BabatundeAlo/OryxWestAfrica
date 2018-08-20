using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OryxWestAfrica.Data;
using OryxWestAfrica.Models;

namespace OryxWestAfrica.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            var client = _context.Clients;
            ViewData["Mee"] = client.ToList();
            var Solu = _context.Solutions;
            ViewData["Sol"] = Solu.ToList();

            var mell = _context.Pictures.Where(m => m.Chcker==true);
           
            return View(mell);
        }
        private readonly ApplicationDbContext _context;

        public HomeController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
           

            
            

            return View();
        }

        public IActionResult Contact()
        {
            
            ViewData["Message"] = "Your contact page.";

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
