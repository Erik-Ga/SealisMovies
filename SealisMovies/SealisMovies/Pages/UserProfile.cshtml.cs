using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SealisMovies.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace SealisMovies.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserProfileModel(Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Models.ProfilePicture ProfilePicture { get; set; }
        [BindProperty]
        public IFormFile UploadedImage { get; set; }
        public List<ProfilePicture> UserProfilePictures { get; set; }
        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserProfilePictures = _context.ProfilePicture.Where(p => p.UserId == userId).ToList();
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            string fileName = string.Empty;
            if (UploadedImage != null)
            {
                Random rnd = new();
                fileName = rnd.Next(0, 10000).ToString() + UploadedImage.FileName;
                var file = "./wwwroot/img/" + fileName;

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }
            }

            ProfilePicture.Image = fileName;
            ProfilePicture.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Add(ProfilePicture);
            await _context.SaveChangesAsync();

            return RedirectToPage("./UserProfile");
        }

        public async Task<IActionResult> OnPostDeletePicture(int id)
        {
            var picture = _context.ProfilePicture.FirstOrDefault(p => p.Id == id);

            if (picture == null)
            {
                return NotFound();
            }

            // Radera filen från wwwroot/img mappen
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", picture.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            //Ta bort bilden från databasen
            _context.ProfilePicture.Remove(picture);
            await _context.SaveChangesAsync();

            return RedirectToPage("./UserProfile");
        }

        public async Task<IActionResult> OnPostUpdatePicture(int id)
        {
            var picture = _context.ProfilePicture.FirstOrDefault(p => p.Id == id);

            if (picture == null)
            {
                return NotFound();
            }

            //Radera nuvarande fil
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", picture.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            //Uppdatera och spara nya profilbilden
            if (UploadedImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(UploadedImage.FileName);
                var file = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }

                picture.Image = fileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./UserProfile");
        }

    }
}
