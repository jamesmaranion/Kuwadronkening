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

            return View();
           
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
