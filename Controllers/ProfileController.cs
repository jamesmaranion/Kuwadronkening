using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Kuwadro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Kuwadro.Data;

namespace Kuwadro.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ProfilePage()
        {
            return View(await _context.artList.ToListAsync());
        }


        public IActionResult UploadArt()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult UploadArt(Art art, IFormFile Image)
        {
            System.Diagnostics.Debug.WriteLine(Image);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            var artwork = new Art()
            {
                UserId = userId,
                Title = art.Title,
                Image = art.Image,
                Description = art.Description,
                CreationDate = DateTime.Now,
                Genre = art.Genre
            };

            if (Image != null)
            {
                if (Image.Length > 0)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/img/ArtWorks", Image.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    artwork.Image = Image.FileName;
                }
            }
            art.UserId = userId;
            art.User = user;
            _context.artList.Add(artwork);
            _context.SaveChanges();

            return RedirectToAction("ProfilePage");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult UploadProfile(ApplicationUser profile, IFormFile Image)
        {
            System.Diagnostics.Debug.WriteLine(Image);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            var prof = new ApplicationUser()
            {
                ProfilePicture = profile.ProfilePicture,
                //Background = profile.Background,
                //Bio = profile.Bio,
            };

            if (Image != null)
            {
                if (Image.Length > 0)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/pfp/profilePic", Image.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    prof.ProfilePicture = Image.FileName;
                }
            }
        
            _context.Add(prof);
            _context.SaveChanges();
            return RedirectToAction("ProfilePage");
        }
        public IActionResult EditProfile()
        {
            return View();
        }

        public IActionResult Profile()
        {
            var Users = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var Arts = _context.artList.Where(p => p.UserId == Users)
                      .ToList();

            var artworks = new Profile()
            {
                ArtList = Arts
            };
            return View(artworks);
        }

        public IActionResult Artwork(int id)
        {
            var Arts = _context.artList.Where(p => p.Id == id)
                .FirstOrDefault();
            
            
            return View(Arts);
        }

    }
}
