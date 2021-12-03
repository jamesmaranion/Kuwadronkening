using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kuwadro.Data;
using Kuwadro.Models;
using System.Security.Claims;

//was suppossed to refactor profile and add this controller but  ran out of time

namespace Kuwadro.Controllers
{
    public class ArtsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtsController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        // GET: Arts
        public  IActionResult Index(int id)
        {
            var art = _context.artList.Include(p => p.User).Where(p => p.Id == id)
    .FirstOrDefault();

            if (art == null)
            {
                return NotFound();
            }
            
            //if id of the user that created it matches the id of the logged in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var canEdit = art.UserId == userId;

            return View(new ArtWork { Art = art, CanEdit = canEdit });
  
        }

        // GET: Arts/Details/5

        
    }
}
