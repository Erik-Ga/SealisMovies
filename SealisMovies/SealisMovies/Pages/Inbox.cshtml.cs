using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SealisMovies.Models;

namespace SealisMovies.Pages
{
    public class InboxModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public InboxModel(Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Message> Messages { get; set; }

        [BindProperty]
        public Models.Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(string recieverid)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (recieverid != null)
            {
                Message = new Models.Message();

                //Här används UserName som UserId, men bara för utseendets skull, annasr blir det hela id-strängen och det är fult :)
                Message.SenderName = currentUser.UserName;
                Message.RecieverId = recieverid;
            }
            Messages = await _context.Message.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string recieverid)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            //Här blir UserId "korrekta" strängen via FK-kopplingen
            Message.SenderId = currentUser.Id;
            Message.RecieverId = recieverid;
            Message.Sent = true;

            _context.Add(Message);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Inbox");
        }
    }
}
