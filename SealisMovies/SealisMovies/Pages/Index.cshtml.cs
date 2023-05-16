using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SealisMovies.Data.Migrations;
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

        public async Task<IActionResult> OnGetAsync(int showid, int deleteid, string recieverid)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (recieverid != null)
            {
                Message = new Models.Message();

                //Här används UserName som UserId, men bara för utseendets skull, annasr blir det hela id-strängen och det är fult :)
                Message.UserId = currentUser.UserName;
                Message.Reciever = recieverid;
            }

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
        public async Task<IActionResult> OnPostAsync(string recieverid)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            //Här blir UserId "korrekta" strängen via FK-kopplingen
            Message.UserId = currentUser.Id;
            Message.Reciever = recieverid;
            Message.Sent= true; 

            _context.Add(Message);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}