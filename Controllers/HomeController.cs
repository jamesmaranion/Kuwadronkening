using Kuwadro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Kuwadro.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Kuwadro.Data;

namespace Kuwadro.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
        }



        public IActionResult Index()
        {
            var Featured = _context.artList.OrderBy(p => p.CreationDate).Take(10)
                   .ToList();

            var Discover = _context.Users.OrderBy(p => p.Id).Take(10)
                   .ToList();

            var artworks = new HomeArt()
            {
                FeaturedArt = Featured,
                DiscoverArtist = Discover

            };
            return View(artworks);

        }

        public IActionResult Search(String q)
        {
            ViewData["Title"] = q;
            return View(_context.Users.Where(u => u.UserName.Contains(q)).OrderBy(u => u.UserName).ToList<ApplicationUser>());  
        }

        public IActionResult Browse()
        {
            var art = _context.artList.ToList<Art>().GroupBy(art => art.Genre);
           
            

            //ViewData["Title"] = "Browse";
            return View(art);
        }



        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("/Identity/Account/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
