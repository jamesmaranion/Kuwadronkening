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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Kuwadro.Areas.Identity.Pages.Account;

namespace Kuwadro.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser>  _signInManager;

        public ProfileController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> ProfilePage()
        {
            return View(await _context.artList.ToListAsync());
        }


        public IActionResult UploadArt()
        {
            return View();
        }

        [AllowAnonymous]

        public IActionResult Index(String id)
        {
        
            var User = _context.Users.Where(u => u.UserName == id).FirstOrDefault();

            if (User == null)
            {
                return NotFound();
            }

            var Arts = _context.artList
                    .Where(art => art.UserId == User.Id)
                    .OrderByDescending(art => art.CreationDate)
                    .ToList();

            var artworks = new Profile()
            {
                User = User,
                ArtList = Arts
            };
            return View(artworks);
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

            return RedirectToAction("Profile");
        }

        public IActionResult UploadProfile()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
 
        public IActionResult UploadProfile(ApplicationUser profile, IFormFile ProfilePicture, IFormFile Background)
        {
            System.Diagnostics.Debug.WriteLine(ProfilePicture);
            System.Diagnostics.Debug.WriteLine(Background);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();

            

            if (ProfilePicture != null)
            {
                if (ProfilePicture.Length > 0)
                {
                    user.ProfilePicture = profile.Background;

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/pfp/profilePic", ProfilePicture.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfilePicture.CopyTo(stream);
                    }
                    user.ProfilePicture = ProfilePicture.FileName;
                }
            }

            if (Background != null)
            {
                if (Background.Length > 0)
                {       
                    user.Background = profile.Background;


                    string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/pfp/coverPic", Background.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Background.CopyTo(stream);
                    }
                    user.Background = Background.FileName;
                }
            }

            if (profile.Bio != null && profile.Bio.Length > 0)
            {
                user.Bio = profile.Bio;
            }

            if (profile.About != null && profile.About.Length > 0)
            {
                user.About = profile.About;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Profile");
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


        [AllowAnonymous]
        public IActionResult Artwork(int id)
        {
            var art = _context.artList.Include(p => p.User).Where(p => p.Id == id)
                .FirstOrDefault();
            //if id of the user that created it matches the id of the logged in user
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var canEdit = art.UserId == userId;
            
            return View(art);
        }

        [AllowAnonymous]
        public IActionResult About(String id)
        {
            var User = _context.Users.Where(u => u.UserName == id).FirstOrDefault();
            return User == null? NotFound(): View(User);
        }


    }
}
