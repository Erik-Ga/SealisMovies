using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SealisMovies.Models;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace SealisMovies.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<ProfilePicture> ProfilePictures { get; set; }
        public List<Models.Discussion> Discussions { get; set; }

        [BindProperty]
        public Models.Discussion Discussion { get; set; }

        [BindProperty]
        public Models.Message Message { get; set; }
        
        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int showid, int deleteid, string messageid)
        {
            //Fråga Micke (Handlar om skicka privat meddelande)
            /*if(messageid != null)
            {
                Message.Reciever = messageid;
            }*/

            ProfilePictures = await _context.ProfilePicture.ToListAsync();

            if (showid != 0)
            {
                Discussion = await _context.Discussions.FindAsync(showid);
            }
            Models.Discussion discussion = await _context.Discussions.FindAsync(deleteid);
            if (deleteid != 0)
            {
                if(discussion != null) 
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + discussion.Image))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + discussion.Image);
                    }
                    _context.Discussions.Remove(discussion);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
            }
            Discussions = await _context.Discussions.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var profilePicture = await _context.ProfilePicture.FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            if (profilePicture != null)
            {
                // Set Discussion.ProfilePicture to the matching ProfilePicture.Image
                Discussion.ProfilePicture = profilePicture.Image;
            }

            string fileName = string.Empty;
            if(UploadedImage != null)
            {
                Random rnd = new();
                fileName = rnd.Next(0, 10000).ToString() + UploadedImage.FileName;
                var file = "./wwwroot/img/" + fileName;
               
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }
            }
            Discussion.Date = DateTime.Now;
            Discussion.Image = fileName;
            Discussion.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Discussion.UserName = User.Identity.Name;

            _context.Add(Discussion);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}